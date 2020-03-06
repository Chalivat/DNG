﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    //MATCHMAKING SERVER
    public string matchmakingIp = "127.0.0.1";
    public int matchmakingPort = 30012;
    //
    //CLIENT SERVER
    public string clientServerIp = "127.0.0.1";
    public int clientServerPort = 30012;
    //

    public int myId = 0;
    public TCP tcp;

    public TCP matchmakingTcp;
    public TCP clientServerTcp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        matchmakingTcp = new TCP();
        tcp = matchmakingTcp;
        ConnectToMatchmakingServer();
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToMatchmakingServer()
    {
        InitializeClientData();

        isConnected = true;
        tcp.Connect(matchmakingIp,matchmakingPort);
    }

    public void ConnectToClientServer()
    {
        clientServerTcp = new TCP();
        tcp = clientServerTcp;
        tcp.Connect(clientServerIp, clientServerPort);
    }

    public void LaunchMatchmaking()
    {
        ClientSend.SearchMatch();
    }

    public class TCP
    {
        public TcpClient socket;

        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;

        public void Connect(string _ip,int _port)
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(_ip, _port,ConnectCallback,socket);
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if(!socket.Connected)
            {
                return;
            }

            stream = socket.GetStream();

            receivedData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if(socket != null)
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

                receivedData.Reset(HandleData(_data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving TCP data: {_ex}");
                Disconnect();
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLenght = 0;

            receivedData.SetBytes(_data);

            if(receivedData.UnreadLength() >= 4)
            {
                _packetLenght = receivedData.ReadInt();
                if(_packetLenght <= 0)
                {
                    return true;
                }
            }

            while (_packetLenght > 0 && _packetLenght <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_packetLenght);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        if(packetHandlers[_packetId] != null)
                        packetHandlers[_packetId](_packet);
                    }
                });

                _packetLenght = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLenght = receivedData.ReadInt();
                    if (_packetLenght <= 0)
                    {
                        return true;
                    }
                }
            }

            if(_packetLenght <= 1)
            {
                return true;
            }

            return false;
        }

        private void Disconnect()
        {
            instance.Disconnect();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;
        }
    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ServerPackets.welcome,ClientHandle.Welcome },
            {(int)ServerPackets.research,ClientHandle.SearchForPlayer},
            {(int)ServerPackets.match,ClientHandle.MatchFind },
            {(int)ServerPackets.register,ClientHandle.RegisterComplet },
            {(int)ServerPackets.login,ClientHandle.LoginComplet }
    };
        Debug.Log("Initialize packet");
    }

    private void Disconnect()
    {
        if(isConnected)
        {
            isConnected = false;
            tcp.socket.Close();

            Debug.Log("Disconnected from server.");
        }
    }
}