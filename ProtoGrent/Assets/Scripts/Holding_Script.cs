using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding_Script : MonoBehaviour
{
    public Main_Script main_script;

    public GameObject board;
    LigneHighlight_Script Highlight_Script;

    public LayerMask mask;

    public Transform Card, Case;
    public Card card;
    bool canPlayCard;
    Camera cam;

    [Space]
    [Header("Holding value")]

    public Vector3 offset;
    public Vector3 holdingRot;

    Vector3 lerpPoint;
    public float lerpSpeed;

    public bool isHolding;

    private void Start()
    {
        cam = Camera.main;
        Highlight_Script = board.GetComponent<LigneHighlight_Script>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && Card != null)
        {
            main_script.removeCartesFromMain(Card.gameObject);
            CheckForCase();
            Highlight_Script.HighlightLine(card.type,Highlight_Script.highlight_Color);
            isHolding = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Highlight_Script.HighlightLine(card.type, Highlight_Script.base_Color);
            if (canPlayCard && Case.GetComponent<Case_Script>().isEmpty)
            {
                ReleaseCard();
            }
            else
            {
                PlaceCardToMain();
            }
            isHolding = false;
        }
    }

    void CheckForCase()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            lerpPoint = hit.point;
            Card.transform.eulerAngles = holdingRot;

            if (hit.transform.CompareTag("Case"))
            {
                Case = hit.transform;
                canPlayCard = Case.GetComponent<Case_Script>().Check(card.type);
            }
        }
        else canPlayCard = false;
        Card.transform.position = Vector3.Lerp(Card.transform.position, lerpPoint + offset, lerpSpeed * Time.deltaTime);
    }

    void ReleaseCard()
    {
        Case.GetComponent<Case_Script>().PlacerCarte(card,Card.gameObject);
        main_script.removeCartesFromMain(Card.gameObject);
    }

    void PlaceCardToMain()
    {
        Card.SetParent(transform);

        Card_Script card_Script = Card.GetComponent<Card_Script>();

        Card.localPosition = card_Script.posInMain;
        Card.localEulerAngles = card_Script.rotInMain;

        main_script.addCarteToMain(Card.gameObject);
    }
}
