using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Script : MonoBehaviour
{
    public GameObject[] typeSymbols;

    public Image image;

    public Vector3 posInMain;
    public Vector3 posInWorld;

    public Vector3 rotInMain;

    public Holding_Script holding;
    public Main_Script main;
    public Card card;
    public ChangeEvent_Script changeEvent;

    private new string name;
    private string description;
    private int damage;

    private int nombrePioche;

    public Text cardDamage;
    public Text cardDescription;

    public Image[] imagesToColour;

    public GameObject descriptionObject;

    public bool isClicked = false;

    Vector3 currentMousePos;
    Vector3 lastMousePos;

    void Start()
    {
        holding = GameObject.FindGameObjectWithTag("Main").GetComponent<Holding_Script>();
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main_Script>();
        changeEvent = GetComponent<ChangeEvent_Script>();

        posInMain = transform.localPosition;
        rotInMain = transform.localEulerAngles;

        posInWorld = transform.position;

        /*if (card != null)
        {
            UpdateVisual();
        }*/
    }

    private void Update()
    {
        if(isClicked)
        {
            lastMousePos = currentMousePos;
            currentMousePos = Input.mousePosition;

            float xScroll = lastMousePos.x - currentMousePos.x;
            float yScroll = lastMousePos.y - currentMousePos.y;

            if(yScroll < -15f)
            {
                BeginDrag();
            }
            else
            {
                transform.parent.localPosition += new Vector3(-xScroll / 100f, 0, 0);
            }
        }
    }

    public void ClickOnCard()
    {
        currentMousePos = Input.mousePosition;
        isClicked = true;
    }

    public void PointerUpOnCard()
    {
        isClicked = false;
        holding.isHolding = false;
    }

    public void SetCard(Card newCard)
    {
        card = newCard;
        UpdateVisual();
    }

    void BeginDrag()
    {
        if (!holding.isHolding && main.mainIsOpen)
        {
            GetComponent<BoxCollider>().enabled = false;

            holding.Card = transform;
            holding.card = card;

            transform.SetParent(null);

            holding.isHolding = true;

            isClicked = false;
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void UpdateVisual()
    {
        for (int i = 0; i < imagesToColour.Length; i++)
        {
            imagesToColour[i].GetComponent<Image>().color = card.contourColor;
        }

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

       image.sprite = card.artwork;

        name = card.name;
        description = card.description;
        damage = card.damage;

        nombrePioche = card.nombrePioche;

        cardDamage.text = damage.ToString();
        cardDescription.text = description;
    }
}
