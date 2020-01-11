using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeEvent_Script : MonoBehaviour
{
    public Button button;
    public EventTrigger eventTrigger;

    CardSelection_Script selection;
    Pioche_Script pioche;
    public bool isOnMain = false;

    void Start()
    {
        selection = GameObject.FindGameObjectWithTag("Main").GetComponent<CardSelection_Script>();
        pioche = GameObject.FindGameObjectWithTag("Pioche").GetComponent<Pioche_Script>();
        button.onClick.AddListener(() => SendGameObject());
    }
    
    void Update()
    {
        if (isOnMain)
        {
            AddHoldingEvent();
        }
        else
        {
            addOnClickEvent();
        }
    }

    public void addOnClickEvent()
    {
        if (isOnMain) return;
    }

    public void AddHoldingEvent()
    {
        if (!isOnMain) return;

    }

    public void SendGameObject()
    {
        Debug.Log("SENDNUD");
        selection.DrawCard(pioche.nombrePioche,this.gameObject);
    }

    public void Void()
    {

    }
}
