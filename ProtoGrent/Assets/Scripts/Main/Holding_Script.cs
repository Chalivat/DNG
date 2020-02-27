using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Holding_Script : MonoBehaviour
{
    public Main_Script main_script;

    public GameObject board;
    LigneHighlight_Script Highlight_Script;

    public LayerMask mask;

    public Transform Carte, Case;
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

    public Vector3 cardPos;
    public Vector3 previousPos;

    private void Start()
    {
        cam = Camera.main;
        Highlight_Script = board.GetComponent<LigneHighlight_Script>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Carte != null)
        {
            CheckForCase();
            RotateCard();

            if(card.type == 4)
            {
                Highlight_Script.DarkerAllLine();
                Highlight_Script.HighLightEffectCaseLine();
                if (card.effectType != Card.EffectType.Encouragement)
                {
                    Highlight_Script.HighLightEffectCaseColonne();
                }

                #region CASE EFFECT HIGHLIGHT

                if (Case != null)
                {
                    Case_Script case_Script = Case.GetComponent<Case_Script>();
                    if (case_Script.isEffect && case_Script.isColonne)
                    {
                        Highlight_Script.HighLightColonne((int)case_Script.pos.x, Highlight_Script.highlight_Color);
                    }
                    else if (case_Script.isEffect && !case_Script.isColonne)
                    {
                        Highlight_Script.HighLightEffectCaseLine();

                        if (card.effectType != Card.EffectType.Encouragement)
                        {
                            Highlight_Script.HighLightEffectCaseColonne();
                            Highlight_Script.HighlightLine((int)case_Script.pos.y, Highlight_Script.highlight_Color, true, true);
                        }
                        else
                        {
                            Highlight_Script.HighlightLine((int)case_Script.pos.y, Highlight_Script.highlight_Color, true, false);
                        }
                    }
                }

                #endregion
            }
            else if(card.type == 3)
            {
                Highlight_Script.HighLightOccupedCase();
            }
            else
            {
                Highlight_Script.DarkerAllLine();
                Highlight_Script.HighlightLine(card.type, Highlight_Script.highlight_Color, false,false);
            }
        }
        if (Input.GetMouseButtonUp(0) && Carte != null)
        {
            if (card.isEspion)
            {
                LigneHighlight_Script.EchangeActiveBoard();
            }

            Highlight_Script.ClearAllCase();

            if (canPlayCard && Case.GetComponent<Case_Script>().isEmpty && card.type != 3 || card.type == 3 && !Case.GetComponent<Case_Script>().isEmpty)
                {
                    ReleaseCard();
                }
                else
                {
                    PlaceCardToMain();
                }

            Carte = null;
            Case = null;
            card = null;
        }
    }

    void CheckForCase()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            lerpPoint = hit.point;
            //Carte.transform.eulerAngles = holdingRot;

            if (hit.transform.CompareTag("Case"))
            {
                Case = hit.transform;
                canPlayCard = Case.GetComponent<Case_Script>().Check(card.type, LigneHighlight_Script.activeBoard);
            }

        }
        else
        {
            canPlayCard = false;
        }
        Carte.transform.position = Vector3.Lerp(Carte.transform.position, lerpPoint + offset, lerpSpeed * Time.deltaTime);
    }

    void ReleaseCard()
    {
        main_script.canPlaceCard = false;
        Case.GetComponent<Case_Script>().PlacerCarte(card);

        main_script.removeCartesFromMain(Carte.gameObject);

        Destroy(Carte.gameObject);
    }

    void PlaceCardToMain()
    {
        Carte.SetParent(transform.GetChild(0));

        Card_Script card_Script = Carte.GetComponent<Card_Script>();

        LerpManager lerpToMain = new LerpManager(Carte.localPosition, card_Script.posInMain, Carte,1f,true,false,LerpCurve.Curve.linear);
        lerpToMain.StartLerp();

        main_script.addCarteToMain(Carte.gameObject);

        Carte.localEulerAngles = card_Script.rotInMain;
    }

    void RotateCard()
    {
        previousPos = cardPos;
        cardPos = Carte.position;

        Vector3 cardvelocity = ((cardPos - previousPos) / Time.deltaTime) * 2;

        Vector3 rot = Vector3.Cross(Vector3.up, cardvelocity);

        Carte.transform.eulerAngles = new Vector3(holdingRot.x + rot.x , rot.y , rot.z);

        //Carte.transform.GetChild(0).LookAt(cam.transform);
    }
}
