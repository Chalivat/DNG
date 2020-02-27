using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Script : MonoBehaviour
{
    public delegate void EndTurn();
    public static EndTurn EndTheTurn;

    public List<GameObject> carteMain = new List<GameObject>();
    public HandPlacement placement;

    public Transform allCartePos;

    public GameObject cardPrefab;

    public float count;

    public bool mainIsOpen;
    public bool canPlaceCard = true;

    private void Update()
    {
        if (canPlaceCard)
        {
            if (Input.GetMouseButtonDown(0))
            {
                count = 0;
            }
            if (Input.GetMouseButton(0))
            {
                count += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0) && count <= .3f)
            {
                Vector3 inputPos = Input.mousePosition;

                float screenWidth = Screen.width;
                float screenHeight = Screen.height;

                float xRatio = inputPos.x / screenWidth;
                float yRatio = inputPos.y / screenHeight;

                if (xRatio >= .05f && xRatio <= .95f && yRatio <= .3f && !mainIsOpen)
                {
                    ShowMain(true);
                }
                else if (xRatio >= .05f && xRatio <= .95f && yRatio > .5f && mainIsOpen)
                {
                    ShowMain(false);
                }
            }

            Vector3 pos = allCartePos.transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, (carteMain.Count * -.25f) - (.15f * carteMain.Count), (carteMain.Count * .25f) + (.15f * carteMain.Count));

            allCartePos.transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        }
    }

    public void UpdateCardOnMain(List<Card> newMain)
    {
        foreach (GameObject card in carteMain)
        {
            Destroy(card);
        }

        carteMain.Clear();
        foreach (Card card in newMain)
        {
            GameObject card_GO = Instantiate(cardPrefab,allCartePos);
            card_GO.GetComponent<Card_Script>().SetCard(card);

            carteMain.Add(card_GO);
        }
        placement.UpdatePlacement();
    }

    public void removeCartesFromMain(GameObject carte)
    {
        carteMain.Remove(carte);
        //placement.UpdatePlacement();
    }

    public void addCarteToMain(GameObject carte)
    {
        if(carte.transform.parent != allCartePos)
        {
            carte.transform.parent = allCartePos;
        }
        carteMain.Add(carte);
        placement.UpdatePlacement();
    }

    public List<GameObject> getMain()
    {
        return carteMain;
    }

    public void ShowMain(bool value)
    {
        if(!value)
        {
            LerpManager lerpMainUp = new LerpManager(allCartePos.localPosition, new Vector3(.5f, -.75f, -1f), allCartePos, 1f, true,false,LerpCurve.Curve.easeInOut);
            lerpMainUp.StartLerp();
            mainIsOpen = false;
        }
        else
        {
            LerpManager lerpMainDown = new LerpManager(allCartePos.localPosition, new Vector3(.5f, 1.15f, -.5f), allCartePos, 1f, true,false,LerpCurve.Curve.easeInOut);
            lerpMainDown.StartLerp();
            mainIsOpen = true;
        }
        placement.UpdatePlacement();
    }

    public void EndMyTurn()
    {
        EndTheTurn();
    }
}
