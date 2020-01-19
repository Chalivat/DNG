using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class DefausseButtonOverlay_Script : MonoBehaviour
{
    public Defausse_Script defausse;

    public int index;

    public float ClickTimer_Start;
    float ClickTimer;

    public bool pointerDown = false;

    private void Start()
    {
        defausse = GameObject.FindGameObjectWithTag("Defausse").GetComponent<Defausse_Script>();

        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => OnPointerDown(true));
        trigger.triggers.Add(pointerDown);

        var poitnerUp = new EventTrigger.Entry();
        poitnerUp.eventID = EventTriggerType.PointerUp;
        poitnerUp.callback.AddListener((e) => addCarteToMain());
        trigger.triggers.Add(poitnerUp);
    }

    private void Update()
    {
        if(pointerDown)
        {
            ClickTimer += Time.deltaTime;
        }
    }

    public void OnPointerDown(bool value)
    {
        ClickTimer = 0;
        pointerDown = value;
    }

    public void addCarteToMain()
    {
        if (defausse.nombrePioche > 0 && ClickTimer <= ClickTimer_Start)
        {
            defausse.ChooseCardFromDefausse(index);
        }
    }
}
