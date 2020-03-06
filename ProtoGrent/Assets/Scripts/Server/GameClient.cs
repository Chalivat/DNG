using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class GameClient : MonoBehaviour
{
    public static GameClient instance;

    //public string ipAddress = "127.0.0.1";
    public int port = 30013;
    public int id;

    private TcpClient client;
    private NetworkStream stream = null;
    private byte[] buffer = new byte[4096];
    private int bytesReceived = 0;
    private string receivedMessage = "";
    private IEnumerator ListenServerMsgCoroutine = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void StartClient(string _ip, int _id)
    {
        if(client != null)
        {
            Debug.Log("Client already started !");
            return;
        }

        id = _id;

        try
        {
            client = new TcpClient();
            client.Connect(_ip, port);

            ListenServerMsgCoroutine = ListenServerMessages();
            StartCoroutine(ListenServerMsgCoroutine);
        }
        catch (Exception _e)
        {
            Debug.Log("Connection error : #" + _e);
        }
    }

    IEnumerator ListenServerMessages()
    {
        if (!client.Connected)
            yield break;

        stream = client.GetStream();

        do
        {
            stream.BeginRead(buffer, 0, buffer.Length, MessageReceived, null);

            if (bytesReceived > 0)
            {
                OnMessageReceived();
                bytesReceived = 0;
            }

            yield return new WaitForSeconds(.5f);

        } while (bytesReceived >= 0 && stream != null);
    }

    private void MessageReceived(IAsyncResult _result)
    {
        if (_result.IsCompleted && client.Connected)
        {
            bytesReceived = stream.EndRead(_result);
            receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
        }
    }

    private void OnMessageReceived()
    {
        Debug.Log("Msg recived on Client: " + receivedMessage);

        //string[] _msg = receivedMessage.Split('\t');

        switch (receivedMessage)
        {
            case "Launch":
                GameServerManager.instance.LauncheGame();
                break;
            case "InitializeGame":

                Debug.Log("Initialize GAME");
                GameManager.instance.BeginMatch();
                //MAKE ALL THE STUFF TO INITIALIZE THE GAME LIKE PIOCHER THE CARD AND CHOISIR QUI COMMENCE

                break;
            default:
                break;
        }
    }

    public void SendMessageToServer(string _msg)
    {
        if (!client.Connected)
            return;

        byte[] msg = Encoding.ASCII.GetBytes(_msg);

        stream.Write(msg, 0, msg.Length);
        Debug.Log("Msg sended to Server: " + _msg);
    }
}
