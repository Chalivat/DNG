using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding_Script : MonoBehaviour
{
    public GameObject board;

    public Transform Card, Case;
    public Card card;
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
            board.GetComponent<LigneHighlight_Script>().HighlightLine(card.type);
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
                Card.transform.position = hit.point;

                canPlayCard = Case.GetComponent<Case_Script>().Check(card.type);
            }
        }
        else canPlayCard = false;
    }

    void ReleaseCard()
    {
        Case.GetComponent<Case_Script>().PlacerCarte(card,Card.gameObject);
    }
}
