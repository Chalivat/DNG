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

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            count = 0;
        }
        if(Input.GetMouseButton(0))
        {
            count += Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(0) && count <= .3f)
        {
            Vector3 inputPos = Input.mousePosition;

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float xRatio = inputPos.x / screenWidth;
            float yRatio = inputPos.y / screenHeight;

            if (xRatio >= .25f && xRatio <= .85f && yRatio <= .3f && !mainIsOpen)
            {
                ShowMain(true);
                placement.UpdatePlacement();
            }
            else if(xRatio >= .25f && xRatio <= .85f && yRatio > .5f && mainIsOpen)
            {
                ShowMain(false);
                placement.UpdatePlacement();
            }
        }

        Vector3 pos = allCartePos.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, (carteMain.Count * -.25f) - (.15f * carteMain.Count), (carteMain.Count * .25f) + (.15f * carteMain.Count));

        allCartePos.transform.localPosition = new Vector3(pos.x, pos.y,pos.z);
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
        placement.UpdatePlacement();
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
            allCartePos.localPosition = new Vector3(0, 0, -1.25f);
            mainIsOpen = false;
        }
        else
        {
            allCartePos.localPosition = new Vector3(0, 1.25f, .25f);
            mainIsOpen = true;
        }
    }

    public void EndMyTurn()
    {
        EndTheTurn();
    }
}
