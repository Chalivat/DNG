using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding_Script : MonoBehaviour
{
    public GameObject board;

    private Transform Card, Case;
    private Card card;
    public bool canPlayCard;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            CheckForCase();
        }
        if (Input.GetMouseButtonUp(0) && canPlayCard)
        {
            ReleaseCard();
        }
    }

    void CheckForCase()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500f))
        {
            if (hit.transform.CompareTag("Case"))
            {
                Case = hit.transform;
                transform.position = hit.point;

                canPlayCard = Case.GetComponent<Case_Script>().Check(card.type);
            }
        }
        else canPlayCard = false;
    }

    void ReleaseCard()
    {
        Case.GetComponent<Case_Script>().PlacerCarte(card);
    }

    public void StartHoldingCard(Card_Script newCard)
    {
        card = newCard.card;

        board.GetComponent<LigneHighlight_Script>().HighlightLine(card.type);
    }
}
