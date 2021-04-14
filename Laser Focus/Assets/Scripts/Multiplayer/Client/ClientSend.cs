using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTPCData(Packet _packet)
    {
        _packet.WriteLength();
        if (Client.instance != null)
        {
            Client.instance.tcp.SendData(_packet);
        }
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets

    public static void WelcomeReceived()
    {
        PlayerConfig.Load();
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.id);
            _packet.Write(PlayerConfig.GetPlayerUsername());

            SendTPCData(_packet);
        }
    }
    public static void CheckDataBase()
    {
        PlayerConfig.CreateJSON();
        PlayerConfig.Load();
        using (Packet _packet = new Packet((int)ClientPackets.checkDatabase))
        {
            _packet.Write(PlayerConfig.GetPlayerID());
            _packet.Write(PlayerConfig.GetPlayerUsername());

            SendTPCData(_packet);
        }
    }

    public static void RegisterUser(string _username)
    {

        using (Packet _packet = new Packet((int)ClientPackets.createUser))
        {
            _packet.Write(_username);

            SendTPCData(_packet);
        }
    }

    public static void SendFriendRequest(int _toUserID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendFriendRequest))
        {
            _packet.Write(PlayerConfig.GetPlayerID());
            _packet.Write(_toUserID);

            SendTPCData(_packet);
            Debug.Log("Friend request sent.");
        }
    }

    public static void AcceptFriendRequest(int _fromUserID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.acceptFriendRequest))
        {
            _packet.Write(PlayerConfig.GetPlayerID());
            _packet.Write(_fromUserID);

            SendTPCData(_packet);
            Debug.Log("Friend request accepted.");
        }
    }

    public static void RejectFriendRequest(int _fromUserID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.rejectFriendRequest))
        {
            _packet.Write(PlayerConfig.GetPlayerID());
            _packet.Write(_fromUserID);

            SendTPCData(_packet);
            Debug.Log("Friend request rejected.");
        }
    }

    public static void RequestAllFriendsList()
    {
        using (Packet _packet = new Packet((int)ClientPackets.friendsListRequest))
        {
            _packet.Write(PlayerConfig.GetPlayerID());

            SendTPCData(_packet);
            Debug.Log("Requesting all friends list...");
        }
    }

    public static void RequestAllFriendRequestsList()
    {
        using (Packet _packet = new Packet((int)ClientPackets.friendRequestsListRequest))
        {
            _packet.Write(PlayerConfig.GetPlayerID());

            SendTPCData(_packet);
            Debug.Log("Requesting all friend requests list...");
        }
    }

    public static void JoinRandomRoom()
    {
        using (Packet _packet = new Packet((int)ClientPackets.joinRandomRoom))
        {
            _packet.Write(PlayerConfig.GetPlayerUsername());
            SendTPCData(_packet);
        }
    }

    public static void CancelMatchmaking()
    {
        using (Packet _packet = new Packet((int)ClientPackets.cancelMatchmaking))
        {
            SendTPCData(_packet);
        }
    }

    public static void SendDeckToServer()
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendDeckToServer))
        {
            _packet.Write(PlayerConfig.GetDeck(PlayerConfig.GetSelectedDeck()).Length);

            for (int i = 0; i < PlayerConfig.GetDeck(PlayerConfig.GetSelectedDeck()).Length; i++)
            {
                _packet.Write(PlayerConfig.GetDeck(PlayerConfig.GetSelectedDeck())[i]);
            }
            SendTPCData(_packet);
        }
    }

    public static void PlayerSuccessfullyLoadedGame()
    {
        using (Packet _packet = new Packet((int)ClientPackets.successfullyLoadedGame))
        {
            SendTPCData(_packet);
        }
    }

    public static void RequestPlayer()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestPlayer))
        {
            SendTPCData(_packet);
        }
    }

    //public static void RequestPlayerTurn()
    //{
    //    using (Packet _packet = new Packet((int)ClientPackets.requestPlayer))
    //    {
    //        SendTPCData(_packet);
    //    }
    //}

    public static void PlaceTowerRequest(Vector2 _pos,Quaternion _rot, int _towerID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.placeTowerRequest))
        {
            _packet.Write(_pos);
            _packet.Write(_rot);
            _packet.Write(_towerID);


            SendTPCData(_packet);
        }
    }

    public static void MatchWinner(int _winner)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendWinner))
        {
            _packet.Write(_winner);
            SendTPCData(_packet);
        }
    }

    public static void PlayerForfeited()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerForfeited))
        {
            SendTPCData(_packet);
        }
    }


    public static void PlayerInput(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerInput))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.id].transform.rotation);

            SendUDPData(_packet);
        }
    }
    #endregion
}
