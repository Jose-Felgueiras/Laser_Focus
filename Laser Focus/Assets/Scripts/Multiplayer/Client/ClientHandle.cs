using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myID = _packet.ReadInt();

        Debug.Log($"Message from Server: {_msg}");
        Client.instance.id = _myID;

        //Connected to Server

        ClientSend.CheckDataBase();

        Client.instance.StopDisconnectTimer();


        //Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SendToMainMenu(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myID = _packet.ReadInt();

        Debug.Log(_msg);

        SceneLoader.instance.LoadLevel(2);
    }

    public static void SendToRegisterMenu(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myID = _packet.ReadInt();

        Debug.Log(_msg);

        SceneLoader.instance.LoadLevel(1);
    }

    public static void RequestNewUsername(Packet _packet)
    {
        string _msg = _packet.ReadString();

        UIManager.instance.ShowLoading(false);
        UIManager.instance.ShowPanel(true);
        UIManager.instance.ShowError(1, 3, _msg);
        Debug.Log("Username already exists");
    }

    public static void ReceiveUserData(Packet _packet)
    {
        string _username = _packet.ReadString();
        int _id = _packet.ReadInt();

        PlayerConfig.SetUsername(_username);
        PlayerConfig.SetID(_id);
        PlayerConfig.Save();

        SceneLoader.instance.LoadLevel(2);
    }

    public static void HandleFriendshipRequest(Packet _packet)
    {
        string _msg = _packet.ReadString();
        FriendManager.instance.PopUpMessage(_msg);
    }

    public static void HandleFriendsListRequest(Packet _packet)
    {
        int _length = _packet.ReadInt();

        FriendEntry[] friendsArray = new FriendEntry[_length];
        int[] _ids = new int[_length];
        string[] _usernames = new string[_length];

        for (int i = 0; i < friendsArray.Length; i++)
        {
            string _username = _packet.ReadString();
            _usernames[i] = _username;
        }
        for (int i = 0; i < friendsArray.Length; i++)
        {
            int _id = _packet.ReadInt();
            _ids[i] = _id;
        }

        FriendManager.instance.SetFriendsList(_length);

        for (int i = 0; i < friendsArray.Length; i++)
        {
            FriendManager.instance.PopulateFriendsList(_ids[i], _usernames[i], i);
        }

        

       
    }

    public static void HandleFriendRequestsListRequest(Packet _packet)
    {
        int _length = _packet.ReadInt();

        FriendRequestEntry[] friendRequestsArray = new FriendRequestEntry[_length];
        int[] _ids = new int[_length];
        string[] _usernames = new string[_length];

        for (int i = 0; i < friendRequestsArray.Length; i++)
        {
            string _username = _packet.ReadString();
            _usernames[i] = _username;
        }
        for (int i = 0; i < friendRequestsArray.Length; i++)
        {
            int _id = _packet.ReadInt();
            _ids[i] = _id;
        }

        FriendManager.instance.SetFriendRequestsList(_length);

        for (int i = 0; i < friendRequestsArray.Length; i++)
        {
            Debug.LogFormat("ID: {0}, User: {1}", _ids[i], _usernames[i]);

            FriendManager.instance.PopulateFriendRequestsList(_ids[i], _usernames[i], i);

        }
    }


    public static void CanceledMatchmaking(Packet _packet)
    {
        MatchmakingManager.instance.ShowMatchmakingPanel(false);
    }

    public static void SendPlayerIntoGame(Packet _packet)
    {
        SceneLoader.instance.LoadLevel(3);
    }

    public static void SetPlayerNumber(Packet _packet)
    {
        int _number = _packet.ReadInt();
        GameManager.instance.SetPlayer(_number);
    }

    public static void StartPlayerTurn(Packet _packet)
    {
        GameManager.instance.GetLocalPlayerController().StartTurn();
    }

    public static void PlaceTower(Packet _packet)
    {
        int _player = _packet.ReadInt();
        Vector2 _pos = _packet.ReadVector2();
        Quaternion _rot = _packet.ReadQuaternion();
        int _towerID = _packet.ReadInt();

        GameManager.instance.PlaceTower(_player, _pos, _rot, _towerID);
    }

    public static void OpponentDisconnected(Packet _packet)
    {
        string _msg = _packet.ReadString();
        InGameHUD.instance.ShowGameOver("You Win", _msg);
    }

    public static void OpponentForfeited(Packet _packet)
    {
        string _msg = _packet.ReadString();
        InGameHUD.instance.ShowGameOver("You Win", _msg);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }
}
