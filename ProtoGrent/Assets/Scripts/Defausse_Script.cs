using System.Collections;
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

    private void Start()
    {
        EffectManager.DefausseEffect += OpenDefausseToPioche;
    }

    private void Update()
    {
        if(isPlacingCard)
        {
            if(Input.GetMouseButtonDown(0))
            {
                CheckCase();
            }
        }
    }

    public void AddBoardCardToDefausse()
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

    void CheckCase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Case"))
            {
                Case_Script case_Script = hit.transform.GetComponent<Case_Script>();
                bool canPlayCard = case_Script.Check(selectedCard.type,LigneHighlight_Script.activeBoard);

                if (canPlayCard)
                {
                    AddCardToBoard(selectedCard, case_Script);
                    isPlacingCard = false;
                    DefausseDisplay.GetComponent<DefausseDisplay_Script>().isOnClick = false;
                }
            }
        }
    }

    void AddCardToBoard(Card card, Case_Script case_Script)
    {
        highlight_Script.HighlightLine(card.type, highlight_Script.base_Color,true,false);

        case_Script.PlacerCarte(card);

        Destroy(choosedCard.gameObject);
        if (nombrePioche > 0 && allCarte.Count > 0)
        {
            ShowAllCard();
        }
    }

    void AddCardsToDefausseFromBoard(Card card)
    {
        allCarte.Add(card);
    }

    void OpenDefausseToPioche(int nombre)
    {
        nombrePioche = nombre;
        ShowAllCard();
    }

    public void ShowAllCard()
    {
        if (allCarte.Count > 0)
        {
            allButton.Clear();
            allObjectCard.Clear();

            DefausseDisplay.gameObject.SetActive(true);

            for (int i = 0; i < allCarte.Count; i++)
            {
                Transform button = Instantiate(ButtonOverlayPrefabs).transform;
                button.SetParent(DefausseDisplay.GetChild(0).GetChild(0));

                button.localPosition = new Vector3(defausseDisplayOffset * i, 0, 0);
                button.localEulerAngles = new Vector3(0, 0, 0);

                button.GetComponent<DefausseButtonOverlay_Script>().index = i;

                allButton.Add(button.gameObject);
            }

            for (int i = 0; i < allCarte.Count; i++)
            {
                GameObject card = Instantiate(CardPrebabs);
                card.GetComponent<Card_Script>().SetCard(allCarte[i]);

                card.transform.parent = DefausseDisplay.GetChild(0);
                card.transform.localPosition = transform.position;

                LerpManager lerpCard = new LerpManager(card.transform.localPosition, new Vector3(defausseDisplayOffset * i, 0, 0), card.transform, 1f, true, false, LerpCurve.Curve.easeInOut);
                lerpCard.StartLerp();

                card.transform.localEulerAngles = Vector3.zero;

                allObjectCard.Add(card);
            }
        }
        else
        {
            main.EndMyTurn();
        }
    }

    public void ChooseCardFromDefausse(int index)
    {
        choosedCard = allObjectCard[index].transform;

        LerpManager lerpToCardPos = new LerpManager(choosedCard.position, CardPos.position, choosedCard, .5f, false, false, LerpCurve.Curve.easeInOut);
        lerpToCardPos.StartLerp();
        choosedCard.SetParent(CardPos);
        //choosedCard.localPosition = Vector3.zero;

        selectedCard = allCarte[index];

        highlight_Script.HighlightLine(allCarte[index].type, highlight_Script.highlight_Color,false,false);

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
