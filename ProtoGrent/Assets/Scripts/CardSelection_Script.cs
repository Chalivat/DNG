using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection_Script : MonoBehaviour
{
    public List<GameObject> nombrePioche = new List<GameObject>();
    public Transform[] cardsPosition;
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

    public void ShowTheCards(int nombre)
    {
        for (int i = 0; i < nombre; i++)
        {
            Instantiate(nombrePioche[i], cardsPosition[i].transform.position, Quaternion.identity);
        }
    }

    public void DrawCard(int nombreP)
    {
        cardCount += 1;
        main.carteMain.Add(nombrePioche[cardCount]);

        if(cardCount == nombreP)
        {
            nombrePioche.Clear();
        }
    }
}
