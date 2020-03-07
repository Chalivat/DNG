using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDecks : MonoBehaviour
{
    public static AllDecks instance;

    public List<Deck> allDeck = new List<Deck>();

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

    public void LoadDeck(string msg)
    {
        string[] msgPart = msg.Split('%');

        Deck deck = new Deck(msgPart[0], int.Parse(msgPart[1]));

        string[] cardsId = msgPart[2].Split('?');

        for (int i = 0; i < cardsId.Length; i++)
        {
            int index = int.Parse(cardsId[i].ToString());
            deck.allCards[i] = AllCards.instance.cardsList[index].card;
        }
        deck.CalculatePower();

        allDeck.Add(deck);
    }
}

[System.Serializable]
public class Deck
{
    private int deckLenght = 30;

    public string name;
    public int type;

    public int deckPower;

    public Card[] allCards;

    public Deck(string _name,int _type)
    {
        name = _name;
        type = _type;

        allCards = new Card[deckLenght];

        deckPower = 0;
    }

    public void CalculatePower()
    {
        //deckPower = allCards[0].cardPower;
        //Debug.Log(allCards[0].cardPower);
        //Debug.Log(deckPower);
        for (int i = 0; i < allCards.Length; i++)
        {
            if(allCards[i] != null)
            deckPower += allCards[i].cardPower;
        }
    }
}
