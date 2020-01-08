using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{
    public Vector3 posInMain;
    public Vector3 rotInMain;

    public Holding_Script holding;
    public Card card;

    private new string name;
    private string description;
    private int damage;

    private int nombrePioche;
    private int nombreDefausse;

    public Text cardName;
    public Text cardDamage;
    public Text cardDescription;

    void Start()
    {
        name = card.name;
        description = card.description;
        damage = card.damage;

        nombrePioche = card.nombrePioche;
        nombreDefausse = card.nombreDefausse;

        cardName.text = name;
        cardDamage.text = damage.ToString();
        cardDescription.text = description;

        holding = GameObject.FindGameObjectWithTag("Main").GetComponent<Holding_Script>();

        posInMain = transform.localPosition;
        rotInMain = transform.localEulerAngles;
    }

    private void OnMouseOver()
    {
        holding.Card = transform;
        holding.card = card;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1);
    }

    private void OnMouseExit()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
    }

    private void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnMouseUp()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
