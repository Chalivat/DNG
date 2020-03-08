using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckPrefab : MonoBehaviour
{
    public Deck deck;

    public Text title;
    public Text power;

    public Image artwork;

    /*public delegate void DeckSelected(Deck _deck);
    public static DeckSelected deckSelected;*/

    public void DrawDeck(Deck _deck,Sprite _sprite)
    {
        deck = _deck;

        title.text = _deck.name;
        power.text = _deck.deckPower.ToString();
        artwork.sprite = _sprite;
    }

    public void Select()
    {
        DeckSelection.instance.selectedDeckPower = deck.deckPower;
        DeckSelection_CardsDisplay.instance.DeleteAllCards();
        DeckSelection_CardsDisplay.instance.UpdateCardList(deck.allCards);
        GameObject.Find("LaunchButton").GetComponent<Button>().interactable = true;
    }
}
