using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 26950;

    public int id = 0;
    public TCP tcp;
    public UDP udp;

    private bool isConnected = false;

    private Coroutine connectionTracker;

    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }

    private void Start()
    {
        tcp = new TCP();
        udp = new UDP();
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectedToServer()
    {
        InitializeClientData();

        isConnected = true;

        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient socket;

        private NetworkStream stream;
        private Packet receiveData;
        private byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            Debug.Log("Attempting to connect");

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);



            if (!socket.Connected)
            {
                instance.StartTimer();
            }
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                Debug.Log("Connection Failed");
                return;
            }


            stream = socket.GetStream();

            receiveData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _byteLenght = stream.EndRead(_result);
                if (_byteLenght <= 0)
                {
                    instance.Disconnect();

                    return;
                }

                byte[] _data = new byte[_byteLenght];
                Array.Copy(receiveBuffer, _data, _byteLenght);

                receiveData.Reset(HandleData(_data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                Disconnect();
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLenght = 0;

            receiveData.SetBytes(_data);

            if (receiveData.UnreadLength() >= 4)
            {
                _packetLenght = receiveData.ReadInt();
                if (_packetLenght <= 0)
                {
                    return true;
                }
            }

            while (_packetLenght > 0 && _packetLenght <= receiveData.UnreadLength())
            {
                byte[] _packetBytes = receiveData.ReadBytes(_packetLenght);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }

                });

                _packetLenght = 0;
                if (receiveData.UnreadLength() >= 4)
                {
                    _packetLenght = receiveData.ReadInt();
                    if (_packetLenght <= 0)
                    {
                        return true;
                    }
                }
            }
            if (_packetLenght <= 1)
            {
                return true;
            }

            return false;
        }

        private void Disconnect()
        {
            instance.Disconnect();
            stream = null;
            receiveData = null;
            receiveBuffer = null;
            socket = null;
        }

        
    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
            //endPoint = new IPEndPoint(IPAddress.Any, instance.port);
        }

        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet _packet = new Packet())
            {
                SendData(_packet);
            }
        }

        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(instance.id);
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via UDP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (_data.Length < 4)
                {
                    instance.Disconnect();

                    return;
                }

                HandleData(_data);
            }
            catch (Exception)
            {

                Disconnect();
            }
        }

        private void HandleData(byte[] _data)
        {
            using (Packet _packet = new Packet(_data))
            {
                int _packetLength = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
        }

        private void Disconnect()
        {
            instance.Disconnect();

            endPoint = null;
            socket = null;
        }
    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ServerPackets.welcome, ClientHandle.Welcome},
            {(int)ServerPackets.sendToMainMenu, ClientHandle.SendToMainMenu},
            {(int)ServerPackets.sendToRegisterMenu, ClientHandle.SendToRegisterMenu},
            {(int)ServerPackets.requestNewUsername, ClientHandle.RequestNewUsername},
            {(int)ServerPackets.sendUserData, ClientHandle.ReceiveUserData},
            {(int)ServerPackets.handledFriendshipRequest, ClientHandle.HandleFriendshipRequest},
            {(int)ServerPackets.handledFriendshipListRequest, ClientHandle.HandleFriendsListRequest},
            {(int)ServerPackets.handledFriendshipRequestListRequest, ClientHandle.HandleFriendRequestsListRequest},
            {(int)ServerPackets.canceledMatchmaking, ClientHandle.CanceledMatchmaking},
            {(int)ServerPackets.sendPlayerIntoGame, ClientHandle.SendPlayerIntoGame},
            {(int)ServerPackets.sendPlayerNumber, ClientHandle.SetPlayerNumber},
            {(int)ServerPackets.startPlayerTurn, ClientHandle.StartPlayerTurn},
            {(int)ServerPackets.placeTower, ClientHandle.PlaceTower},
            {(int)ServerPackets.opponentDisconnected, ClientHandle.OpponentDisconnected},
            {(int)ServerPackets.opponentForfeited, ClientHandle.OpponentForfeited},

            {(int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer},
            {(int)ServerPackets.playerPosition, ClientHandle.PlayerPosition},
            {(int)ServerPackets.playerRotation, ClientHandle.PlayerRotation}

        };
        Debug.Log("Initialized Packets");
    }

    private void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            if (tcp != null)
            {
                if (tcp.socket != null)
                {
                    tcp.socket.Close();
                }
            }
            if (udp != null)
            {
                if (udp.socket != null)
                {
                    udp.socket.Close();
                }
            }
           

            Debug.Log("Disconnected from Server");
        }
    }

    private void StartTimer()
    {
        connectionTracker = StartCoroutine(ConnectionAttemptTimer(15f));
    }
    public void StopDisconnectTimer()
    {
        StopCoroutine(connectionTracker);
    }

    private static IEnumerator ConnectionAttemptTimer(float _failDelay)
    {
        float t = 0;
        while (t < _failDelay)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Unable to connect");
        UIManager.instance.panel.SetActive(true);
        SceneLoader.instance.ShowLoadingScreen(false);
        UIManager.instance.ShowError(1, 3, "Unable to connect with Server");
        instance.Disconnect();
        yield return null;
    }
}
