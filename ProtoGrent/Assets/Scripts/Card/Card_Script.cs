using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{
    public GameObject[] typeSymbols;

    public MeshRenderer planeRenderer;

    public Vector3 posInMain;
    public Vector3 posInWorld;

    public Vector3 rotInMain;

    public Holding_Script holding;
    public Card card;
    public ChangeEvent_Script changeEvent;

    private new string name;
    private string description;
    private int damage;

    private int nombrePioche;
    private int nombreDefausse;

    public Text cardDamage;
    public Text cardDescription;

    public GameObject descriptionObject;
    public bool asDescription = true;

    void Start()
    {
        if(asDescription)
        {
            descriptionObject.SetActive(true);
        }
        else
        {
            descriptionObject.SetActive(false);
        }

        switch(card.type)
        {
            case 0:
                typeSymbols[0].SetActive(true);
                break;
            case 1:
                typeSymbols[1].SetActive(true);
                break;
            case 2:
                typeSymbols[2].SetActive(true);
                break;
            default:
                break;
        }

        planeRenderer.material.SetColor("_FireColor",card.fireColor);
        planeRenderer.material.SetColor("_BorderColor", card.borderColor);
        planeRenderer.material.SetTexture("_Artwork", card.artwork);

        name = card.name;
        description = card.description;
        damage = card.damage;

        nombrePioche = card.nombrePioche;
        nombreDefausse = card.nombreDefausse;

        cardDamage.text = damage.ToString();
        cardDescription.text = description;

        holding = GameObject.FindGameObjectWithTag("Main").GetComponent<Holding_Script>();
        changeEvent = GetComponent<ChangeEvent_Script>();

        posInMain = transform.localPosition;
        rotInMain = transform.localEulerAngles;

        posInWorld = transform.position;
    }

    public void ClickOnCard()
    {
        if (!holding.isHolding)
        {
            GetComponent<BoxCollider>().enabled = false;

            holding.Card = transform;
            holding.card = card;

            transform.SetParent(null);
            //transform.position = posInWorld;

            holding.isHolding = true;
        }
    }

    public void PointerUpOnCard()
    {
        holding.isHolding = false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
