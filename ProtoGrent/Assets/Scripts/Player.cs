using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNombre;

    public List<Card> allCard = new List<Card>();

    public Board_Script playingBoard;

    public int point;

    public bool asPassed = false;
    public int life = 2;

    public void SetAllCard(List<Card> cards)
    {
        allCard.Clear();
        foreach (Card card in cards)
        {
            allCard.Add(card);
        }
    }
}
