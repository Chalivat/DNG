using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoriqueManager : MonoBehaviour
{
    public HistoriqueDisplay historiqueDisplay;
    public List<PlayerCoup> allPlayersCoup;

    private void Start()
    {
        Case_Script.playerAction += AddPlayerCoup;
        GameManager.newmanche += ClearAllCoup;
    }

    public List<PlayerCoup> GetAllPlayerCoup()
    {
        return allPlayersCoup;
    }

    public void AddPlayerCoup(PlayerCoup coup)
    {
        allPlayersCoup.Add(coup);
        historiqueDisplay.AddCoup(coup);
    }

    public void ClearAllCoup()
    {
        allPlayersCoup.Clear();
        historiqueDisplay.ClearHistorique();
    }
}

[System.Serializable]
public class PlayerCoup
{
    public Case_Script caseFill;
    public Card cardPlaced;
    public uint playerNumber;

    public PlayerCoup(Case_Script caseFill,Card cardPlaced,uint playerNumber)
    {
        this.cardPlaced = cardPlaced;
        this.playerNumber = playerNumber;
        this.caseFill = caseFill;
    }
}
