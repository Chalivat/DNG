using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{
    public Vector3 posInMain;
    public Vector3 posInWorld;

    public Vector3 rotInMain;

    public Holding_Script holding;
    public Card card;
    public ChangeEvent_Script changeEvent;

    private new string name;
    private string description;
    private int damage;
    private int type;

    private int nombrePioche;
    private int nombreDefausse;

    public Text cardDamage;
    public Text cardDescription;
    public Text cardType;

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

        name = card.name;
        description = card.description;
        damage = card.damage;
        type = card.type;

        nombrePioche = card.nombrePioche;
        nombreDefausse = card.nombreDefausse;

        cardDamage.text = damage.ToString();
        cardDescription.text = description;
        cardType.text = type.ToString();

        holding = GameObject.FindGameObjectWithTag("Main").GetComponent<Holding_Script>();
        changeEvent = GetComponent<ChangeEvent_Script>();

        posInMain = transform.localPosition;
        rotInMain = transform.localEulerAngles;

        posInWorld = transform.position;
    }

    /*private void OnMouseOver()
    {
        if (!holding.isHolding)
        {
            holding.Card = transform;
            holding.card = card;
           //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1);
        }
    }*/

    /*private void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnMouseUp()
    {
        GetComponent<BoxCollider>().enabled = true;
    }*/

    public void ClickOnCard()
    {
        Debug.Log("POINTER DOWN");
        if (!holding.isHolding)
        {
            GetComponent<BoxCollider>().enabled = false;

            holding.Card = transform;
            holding.card = card;

            transform.SetParent(null);
            transform.position = posInWorld;

            holding.isHolding = true;
        }
    }

    public void PointerUpOnCard()
    {
        Debug.Log("POINTER UP");

        holding.isHolding = false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
