using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelection_CardsDisplay : MonoBehaviour
{
    public static DeckSelection_CardsDisplay instance;

    public Transform allCardsParent;

    public GameObject cardsDisplayPrefab;
    private List<GameObject> cardsDrawed = new List<GameObject>();

    public float listPadding;
    public float cardsPadding;

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

    public void UpdateCardList(Card[] _cards)
    {
        Vector2 frameSize = allCardsParent.GetComponent<RectTransform>().sizeDelta;

        frameSize.x -= listPadding;
        frameSize.y -= listPadding;

        cardsDrawed .Clear();
        //allDeckCards = _cards;
        for (int i = 0; i < _cards.Length; i++)
        {
            if (_cards[i] == null)
                return;

            GameObject card = Instantiate(cardsDisplayPrefab, allCardsParent);
            card.GetComponent<Menu_CardDisplay>().Initialize(_cards[i]);

            if (i < _cards.Length / 2)
                card.GetComponent<RectTransform>().anchoredPosition = new Vector3(75, -listPadding - cardsPadding * i);
            else
                card.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, -listPadding - cardsPadding * (i - _cards.Length / 2), 0);
            cardsDrawed.Add(card);
        }
    }

    public void DeleteAllCards()
    {
        for (int i = 0; i < cardsDrawed.Count; i++)
        {
            Destroy(cardsDrawed[i]);
        }
    }
}
