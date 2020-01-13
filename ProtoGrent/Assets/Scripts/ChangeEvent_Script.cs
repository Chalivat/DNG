using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeEvent_Script : MonoBehaviour
{
    public Button button;

    CardSelection_Script selection;
    Pioche_Script pioche;
    public bool isOnMain = false;

    void Start()
    {
        selection = GameObject.FindGameObjectWithTag("Main").GetComponent<CardSelection_Script>();
        pioche = GameObject.FindGameObjectWithTag("Pioche").GetComponent<Pioche_Script>();
        //button.onClick.AddListener(() => SendGameObject());
        /*trigger = GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { SendGameObject((PointerEventData)data); });
        trigger.triggers.Add(entry);*/
    }
    
    void Update()
    {

    }

    public void Void()
    {

    }
}
