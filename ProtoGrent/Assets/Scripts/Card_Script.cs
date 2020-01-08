using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{
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
    }
}
