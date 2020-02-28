using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{
    public Vector3 posInMain;
    public Vector3 rotInMain;

    public Card card;

    public delegate void ClickedEvent(bool value,Transform trans,Card card);
    public static ClickedEvent ClickTheCard;

    [Space]
    [Header("Visual")]

    public Image artwork;

    public Text cardDamage;
    public Text cardDescription;

    public Image[] imagesToColour;
    public SpriteRenderer[] spriteToColor;

    public Text descriptionText;

    public GameObject descriptionObject;

    public GameObject[] typeSymbols;

    void Start()
    {
        posInMain = transform.localPosition;
        rotInMain = transform.localEulerAngles;

        transform.GetComponentInChildren<Canvas>().worldCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    public void ClickOnCard()
    {
        ClickTheCard(true,transform ,card);
        posInMain = transform.localPosition;
    }

    public void PointerUpOnCard()
    {
        ClickTheCard(false,null,null);
    }

    public void SetCard(Card newCard)
    {
        card = newCard;
        UpdateVisual();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void UpdateVisual()
    {
        artwork.sprite = card.artwork;

        for (int i = 0; i < imagesToColour.Length; i++)
        {
            imagesToColour[i].color = card.contourColor;
        }
        for (int i = 0; i < spriteToColor.Length; i++)
        {
            spriteToColor[i].color = card.contourColor;
        }

        descriptionText.color = card.contourColor;

        if (card.asDescription)
        {
            descriptionObject.SetActive(true);
        }
        else
        {
            descriptionObject.SetActive(false);
        }

        switch (card.type)
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
            case 4:
                break;
            default:
                break;
        }

        cardDamage.text = card.damage.ToString();
        cardDescription.text = card.description;
    }
}
