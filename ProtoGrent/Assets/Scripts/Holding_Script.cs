using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding_Script : MonoBehaviour
{
    private GameObject Card, Case;
    private bool canPlayCard;
    public Camera cam;

    void Start()
    {
        
    }
   
    void Update()
    {
        CheckForCase();
    }

    void CheckForCase()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500f))
        {
            if (hit.transform.CompareTag("Case"))
            {
                Case = hit.transform.gameObject;
                //canPlayCard = Case.Check(Card);
            }
        }

    }

    void PlayCard()
    {
        if (Case)
        {
            if (canPlayCard)
            {
                //Case.PlayCard
            }
        }
    }
}
