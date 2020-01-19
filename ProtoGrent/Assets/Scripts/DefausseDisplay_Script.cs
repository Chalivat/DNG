using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefausseDisplay_Script : MonoBehaviour
{
    public Defausse_Script defausse;

    public bool isOnClick;

    public bool drag;

    public Transform allCard;

    public float scrollSensitivity = .1f;
    float lastPos, currentPos;

    public float exitTimer_Start;
    float exitTimer;

    private void Update()
    {
        if(isOnClick)
        {
            exitTimer += Time.deltaTime;

            lastPos = currentPos;
            currentPos = Input.mousePosition.x;

            allCard.localPosition += new Vector3((currentPos - lastPos) * scrollSensitivity, 0, 0);

            float xposClamped = Mathf.Clamp(allCard.localPosition.x, -allCard.childCount * 1.25f, 0);

            allCard.localPosition = new Vector3(xposClamped, 0, 0);
        }
        if(Input.GetMouseButtonDown(0))
        {
            isOnClick = true;
            currentPos = Input.mousePosition.x;
            exitTimer = 0;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isOnClick = false;
            if (exitTimer <= exitTimer_Start && defausse.nombrePioche <= 0)
            {
                CloseDefausse();
            }
        }
    }

    public void BeginDrag()
    {
        drag = true;

        currentPos = Input.mousePosition.x;
    }

    public void EndDrag()
    {
        drag = false;
    }

    public void CloseDefausse()
    {
        allCard.transform.localPosition = Vector3.zero;
        int childCount = allCard.childCount;
        for (int i = 1; i < childCount; i++)
        {
            Destroy(allCard.GetChild(i).gameObject);
        }

        defausse.DestroyButton();

        gameObject.SetActive(false);
    }
}
