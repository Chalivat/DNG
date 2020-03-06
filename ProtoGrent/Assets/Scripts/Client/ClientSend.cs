using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using(Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);

            SendTCPData(_packet);
        }
    }

    public static void SearchMatch()
    {
        using (Packet _packet = new Packet((int)ClientPackets.researchReceived))
        {
            _packet.Write(Client.instance.myId);
            //_packet.Write(Client.instance);
            _packet.Write(UIManager.instance.DeckSelectionField.text);

            SendTCPData(_packet);
        }
    }

    public static void Register()
    {
        using (Packet _packet = new Packet((int)ClientPackets.register))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.Register_usernameField.text);
            _packet.Write(UIManager.instance.Register_passwordField.text);
            _packet.Write(UIManager.instance.Register_emailField.text);

            SendTCPData(_packet);
        }
    }

    public static void Login()
    {
        using (Packet _packet = new Packet((int)ClientPackets.login))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.Login_usernameField.text);
            _packet.Write(UIManager.instance.Login_passwordField.text);

            SendTCPData(_packet);
        }
    }
    #endregion
}
