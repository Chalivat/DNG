using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        UIManager.instance.GoToMain();
        ClientSend.WelcomeReceived();
    }

    public static void SearchForPlayer(Packet _packet)
    {
        int _power = _packet.ReadInt();

        Debug.Log($"Message from server: {_power}");
    }

    public static void MatchFind(Packet _packet)
    {
        string _adversaire = _packet.ReadString();
        string _ip = _packet.ReadString().Split(':')[0];
        int _id = _packet.ReadInt();

        if (_id == 1)
        {
            GameServerManager.instance.CreateLocalServer();
            GameServerManager.instance.CreateClient("127.0.0.1",0);
        }
        else
        {
            GameServerManager.instance.CreateClient(_ip,1);
        }

        Debug.Log($"Message from server: new challenger approch: {_adversaire} ip server {_ip}!");
    }
    public static void RegisterComplet(Packet _packet)
    {
        string msg = _packet.ReadString();

        Debug.Log($"Message from server: {msg}");
    }

    public static void LoginComplet(Packet _packet)
    {
        string msg = _packet.ReadString();
        string[] msgPart = msg.Split('\t');

        if(bool.Parse(msgPart[2]))
        {
            UIManager.instance.GoToDeckSelection();
        }

        Debug.Log($"Message from server: {msgPart[0]} AS {msgPart[1]}");
        UIManager.instance.label.text = msgPart[1];
    }

}
