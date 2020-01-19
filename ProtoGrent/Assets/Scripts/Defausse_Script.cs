﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defausse_Script : MonoBehaviour
{
    public Main_Script main;

    public List<Card> allCarte = new List<Card>();
    public List<GameObject> allObjectCard = new List<GameObject>();

    public List<GameObject> allButton = new List<GameObject>();

    public Board_Script board1, board2;

    public GameObject CardPrebabs;
    public Transform DefausseDisplay;

    public GameObject ButtonOverlayPrefabs;

    public LigneHighlight_Script highlight_Script;

    public Transform CardPos;

    public float defausseDisplayOffset = 1.25f;

    public int nombrePioche;

    public bool isPlacingCard;

    Card selectedCard;
    Transform choosedCard;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            foreach (Card item in board1.GetCardPlaced())
            {
                AddCardsToDefausseFromBoard(item);
            }
            foreach (Card item in board2.GetCardPlaced())
            {
                AddCardsToDefausseFromBoard(item);
            }
        }
        if(isPlacingCard)
        {
            if(Input.GetMouseButtonDown(0))
            {
                CheckCase();
            }
        }
    }

    void CheckCase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Case"))
            {
                Case_Script case_Script = hit.transform.GetComponent<Case_Script>();
                AddCardToBoard(selectedCard, case_Script);
                isPlacingCard = false;
            }
        }
    }

    void AddCardToBoard(Card card, Case_Script case_Script)
    {
        highlight_Script.HighlightLine(card.type, highlight_Script.base_Color);

        case_Script.PlacerCarte(card);

        Destroy(choosedCard.gameObject);
        if(nombrePioche > 0 && allCarte.Count > 0)
        {
            ShowAllCard();
        }
    }

    void AddCardsToDefausseFromBoard(Card card)
    {
        allCarte.Add(card);
    }

    public void ShowAllCard()
    {
        allButton.Clear();
        allObjectCard.Clear();

        DefausseDisplay.gameObject.SetActive(true);

        for (int i = 0; i < allCarte.Count; i++)
        {
            Card_Script card = Instantiate(CardPrebabs).GetComponent<Card_Script>();
            card.card = allCarte[i];
            card.UpdateVisual();

            Transform button = Instantiate(ButtonOverlayPrefabs).transform;
            button.SetParent(DefausseDisplay.GetChild(0).GetChild(0));
            button.localPosition = new Vector3(defausseDisplayOffset * i, 0, 0);
            button.localEulerAngles = new Vector3(0, 0, 0);

            button.GetComponent<DefausseButtonOverlay_Script>().index = i;

            allButton.Add(button.gameObject);

            card.transform.parent = DefausseDisplay.GetChild(0);
            card.transform.localPosition = new Vector3(defausseDisplayOffset * i,0,0);
            card.transform.localEulerAngles = Vector3.zero;

            allObjectCard.Add(card.gameObject);
        }
    }

    public void ChooseCardFromDefausse(int index)
    {
        choosedCard = allObjectCard[index].transform;
        choosedCard.SetParent(CardPos);
        choosedCard.localPosition = Vector3.zero;

        selectedCard = allCarte[index];

        highlight_Script.HighlightLine(allCarte[index].type, highlight_Script.highlight_Color);

        nombrePioche--;

        allButton[index].gameObject.SetActive(false);

        allCarte.Remove(allObjectCard[index].GetComponent<Card_Script>().card);

        DefausseDisplay.GetComponent<DefausseDisplay_Script>().CloseDefausse();

        isPlacingCard = true;
    }

    public void DestroyButton()
    {
        for (int i = 0; i < allButton.Count; i++)
        {
            Destroy(allButton[i]);
        }
    }
}