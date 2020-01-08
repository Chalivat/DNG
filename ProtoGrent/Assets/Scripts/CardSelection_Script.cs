using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection_Script : MonoBehaviour
{
    public List<GameObject> nombrePioche = new List<GameObject>();
    Main_Script main;
    int nombreCarte;
    int cardCount;

    private void Start()
    {
        main = GetComponent<Main_Script>();
    }

    public void AddToList(GameObject carte)
    {
        nombrePioche.Add(carte);
    }

    public void ShowTheCards()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cardCount = 1;
            DrawCard();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cardCount = 2;
            DrawCard();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cardCount = 3;
            DrawCard();
        }
    }

    public void DrawCard()
    {
        main.carteMain.Add(nombrePioche[cardCount]);
        nombrePioche.Clear();
    }
}
