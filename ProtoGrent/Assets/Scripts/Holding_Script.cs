using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding_Script : MonoBehaviour
{
    public GameObject board;
    LigneHighlight_Script Highlight_Script;

    public float lerpSpeed;

    public LayerMask mask;

    public Transform Card, Case;
    public Card card;
    public bool canPlayCard;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
        Highlight_Script = board.GetComponent<LigneHighlight_Script>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && Card != null)
        {
            CheckForCase();
            Highlight_Script.HighlightLine(card.type,Highlight_Script.highlight_Color);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Highlight_Script.HighlightLine(card.type, Highlight_Script.base_Color);
            if (canPlayCard)
            {
                ReleaseCard();
            }
            else
            {
                PlaceCardToMain();
            }
        }
    }

    void CheckForCase()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            Card.transform.position = Vector3.Lerp(Card.transform.position, hit.point, lerpSpeed*Time.deltaTime);
            Card.transform.eulerAngles = new Vector3(90, 0, 0);

            if (hit.transform.CompareTag("Case"))
            {
                Case = hit.transform;
                canPlayCard = Case.GetComponent<Case_Script>().Check(card.type);
            }
        }
        else canPlayCard = false;
    }

    void ReleaseCard()
    {
        Case.GetComponent<Case_Script>().PlacerCarte(card,Card.gameObject);
    }

    void PlaceCardToMain()
    {
        Card_Script card_Script = Card.GetComponent<Card_Script>();

        Card.localPosition = card_Script.posInMain;
        Card.localEulerAngles = card_Script.rotInMain;
    }
}
