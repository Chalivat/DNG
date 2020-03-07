using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class GameServer : MonoBehaviour
{
    public string _ipAdress = "127.0.0.1";
    public int _port = 30013;

    public bool[] clientStatus = new bool[2];

    private GameServer_Client _client1 = new GameServer_Client();
    private GameServer_Client _client2 = new GameServer_Client();
    private TcpListener _server;
    private byte[] _buffer = new byte[4096];

    private int _bytesReceived1;
    private string _messageReceived1;

    private int _bytesReceived2;
    private string _messageReceived2;

    private IEnumerator ListenClientMsgsCoroutine = null;

    public void InitializeServer()
    {
        _server = new TcpListener(IPAddress.Any, _port);
        _server.Start();
        _server.BeginAcceptTcpClient(ClientConnectCallBack, null);

        Debug.Log("Start Game server");
    }

    private void Update()
    {
        if (_client1.client != null && _client2.client != null && ListenClientMsgsCoroutine == null)
        {
            ListenClientMsgsCoroutine = ListenClientMessage();

            StartCoroutine(ListenClientMsgsCoroutine);
        }
    }

    private void ClientConnectCallBack(IAsyncResult _result)
    {
        if (_client1.client == null)
        {
            _client1.client = _server.EndAcceptTcpClient(_result);
            _client1.stream = _client1.client.GetStream();
            Debug.Log("Client 1 connected");
            _server.BeginAcceptTcpClient(ClientConnectCallBack, null);
        }
        else if (_client2.client == null)
        {
            _client2.client = _server.EndAcceptTcpClient(_result);
            _client2.stream = _client2.client.GetStream();
            Debug.Log("Client 2 connected");

            SendMessageToBothClient("Launch");
            //_server.BeginAcceptTcpClient(ConnectCallBack, null);
        }
        else
            Debug.Log("Already two player connected to the server");
    }

    IEnumerator ListenClientMessage()
    {
        _bytesReceived1 = 0;
        _bytesReceived2 = 0;

        _buffer = new byte[4096];

        _client1.stream = _client1.client.GetStream();
        _client2.stream = _client2.client.GetStream();

        do
        {
            #region CLIENT 1
            //Start Async Reading from Client and manage the response on MessageReceived function
            _client1.stream.BeginRead(_buffer, 0, _buffer.Length, MessageReceived1, _client1.stream);
            _client2.stream.BeginRead(_buffer, 0, _buffer.Length, MessageReceived2, _client2.stream);

            //If there is any msg, do something
            if (_bytesReceived1 > 0)
            {
                OnMessageReceived(_messageReceived1);
                _bytesReceived1 = 0;
            }

            if(_bytesReceived2 > 0)
            {
                OnMessageReceived(_messageReceived2);
                _bytesReceived2 = 0;
            }
            #endregion
            yield return new WaitForSeconds(.5f);

        } while (_bytesReceived1 >= 0 && _bytesReceived2 >= 0 && _client1.stream != null && _client2.stream != null);
    }

    private void SendMessageToClient(string _msg, GameServer_Client client)
    {    
        if (client.stream == null)
        {
            Debug.Log("Socket Error: Start at least one client first");
            return;
        }
       
        byte[] msgOut = Encoding.ASCII.GetBytes(_msg); //Encode message as bytes
        client.stream.Write(msgOut, 0, msgOut.Length);
        Debug.Log("Msg sended to Client: " + _msg);
    }

    private void SendMessageToBothClient(string _msg)
    {
        if (_client1.stream == null || _client2.stream == null)
        {
            Debug.Log("Socket Error: Start at least one client first");
            return;
        }

        byte[] msgOut = Encoding.ASCII.GetBytes(_msg); //Encode message as bytes

        _client1.stream.Write(msgOut, 0, msgOut.Length);
        _client2.stream.Write(msgOut, 0, msgOut.Length);

        Debug.Log("Msg sended to BOTH Client: " + _msg);
    }

    private void OnMessageReceived(string receivedMessage)
    {
        Debug.Log("Msg recived on Server: " + receivedMessage);

        string[] _msg = receivedMessage.Split('\t');

        switch (_msg[0])
        {
            case "Launched":
                clientStatus[int.Parse(_msg[1])] = true;

                if (clientStatus[0] && clientStatus[1])
                {
                    Debug.Log("IS LAUNCHED FOR BOTH");
                    SendMessageToBothClient("InitializeGame");
                    clientStatus[0] = false;
                    clientStatus[1] = false;
                }
                break;
            case "Initialized":
                clientStatus[int.Parse(_msg[1])] = true;

                if (clientStatus[0] && clientStatus[1])
                {

                    clientStatus[0] = false;
                    clientStatus[1] = false;
                }
                break;
            default:
                break;
        }
    }
    private void MessageReceived1(IAsyncResult result)
    {
        if (result.IsCompleted && _client1.client.Connected && _client2.client.Connected)
        {
            //build message received from client
            _bytesReceived1 = _client1.stream.EndRead(result);                            //End async reading
            _messageReceived1 = Encoding.ASCII.GetString(_buffer, 0, _bytesReceived1); //De-encode message as string
        }
    }

    private void MessageReceived2(IAsyncResult result)
    {
        if (result.IsCompleted && _client1.client.Connected && _client2.client.Connected)
        {
            //build message received from client
            _bytesReceived2 = _client1.stream.EndRead(result);                            //End async reading
            _messageReceived2 = Encoding.ASCII.GetString(_buffer, 0, _bytesReceived2); //De-encode message as string
        }
    }

    public class GameServer_Client
    {
        public static int dataBufferSize = 4096;

        public TcpClient client;
        public NetworkStream stream;

    }
}