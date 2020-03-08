using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_CardDisplay : MonoBehaviour
{
    public Card card;

    public Image gem;
    public Image border;

    public Image artwork;
    public Text damage;

    public void Initialize(Card _card)
    {
        card = _card;

        gem.color = card.contourColor;
        border.color = card.contourColor;

        artwork.sprite = card.artwork;
        if (card.damage > 0)
            damage.text = card.damage.ToString();
    }
}
