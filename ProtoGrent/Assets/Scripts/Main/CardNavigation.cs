using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardNavigation : MonoBehaviour
{
    public Holding_Script holding;
    public Main_Script main;

    public Transform allCard;

    public bool CardIsClicked = false;
    public bool CardAsPopUp = false;

    public float xSensitivity = 1;

    public float popUpTimer;
    float Timer;

    Vector3 firstPos;
    Vector3 currentMousePos;
    Vector3 lastMousePos;

    Card card;
    Transform clickedCardTrans;

    // Start is called before the first frame update
    void Start()
    {
        Card_Script.ClickTheCard += ClickOnCard;
    }

    private void Update()
    {
        if (CardIsClicked)
        {
            lastMousePos = currentMousePos;
            currentMousePos = Input.mousePosition;

            Timer -= Time.deltaTime;

            if(Timer <= 0 && currentMousePos == firstPos && !CardAsPopUp)
            {
                CardAsPopUp = true;
                LerpManager popUpLerp = new LerpManager(clickedCardTrans.position, clickedCardTrans.position - clickedCardTrans.forward *.65f + clickedCardTrans.up * .65f, clickedCardTrans,.5f,false,false,LerpCurve.Curve.easeInOut);
                popUpLerp.StartLerp();
            }
            if (Mathf.Abs(lastMousePos.x - currentMousePos.x) > 1 && !CardAsPopUp)
            {
                allCard.Translate(Vector3.right * (currentMousePos.x - lastMousePos.x) * xSensitivity * Time.deltaTime,Space.Self);

                Vector3 pos = allCard.transform.localPosition;
                pos.x = Mathf.Clamp(pos.x, (allCard.transform.childCount * -.25f) - (.15f * allCard.transform.childCount), (allCard.transform.childCount * .25f) + (.15f * allCard.transform.childCount));

                allCard.transform.localPosition = pos;
            }
            if (currentMousePos.y >= Screen.height * .55f && CardAsPopUp)
            {
                BeginDrag();
            }
        }
    }

    public void ClickOnCard(bool value,Transform trans,Card card)
    {
        CardIsClicked = value;
        if (!value)
        {
            holding.isHolding = false;
            if (CardAsPopUp)
            {
                LerpManager popUpLerp = new LerpManager(clickedCardTrans.localPosition, clickedCardTrans.GetComponent<Card_Script>().posInMain, clickedCardTrans, .5f,true,true,LerpCurve.Curve.linear);
                popUpLerp.StartLerp();
                CardAsPopUp = false;
            }
        }

        firstPos = Input.mousePosition;
        currentMousePos = firstPos;

        this.card = card;
        clickedCardTrans = trans;

        Timer = popUpTimer;
    }

    void BeginDrag()
    {
        if (!holding.isHolding && main.mainIsOpen)
        {
            clickedCardTrans.GetComponent<BoxCollider>().enabled = false;

            holding.Carte = clickedCardTrans;
            holding.card = card;

            clickedCardTrans.SetParent(null);

            holding.isHolding = true;

            CardIsClicked = false;
            CardAsPopUp = false;

            if (card.isEspion)
            {
                LigneHighlight_Script.EchangeActiveBoard();
            }
            main.removeCartesFromMain(clickedCardTrans.gameObject);
            main.ShowMain(false);
        }
    }
}
