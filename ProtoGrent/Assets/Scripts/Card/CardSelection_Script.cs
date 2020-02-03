using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection_Script : MonoBehaviour
{
    public List<CartePioche> nombrePioche = new List<CartePioche>();
    public List<CartePioche> tmpPioche = new List<CartePioche>();

    public List<Transform> pickedUp;
    public Transform[] cardsPosition;

    public GameObject cardPrefab;

    Main_Script main;
    Pioche_Script pioche;
    int nombreCarte;
    int cardCount;

    bool endTurnAfterPickUp = true;

    private void OnEnable()
    {
        main = GetComponent<Main_Script>();
        pioche = GameObject.FindGameObjectWithTag("Pioche").GetComponent<Pioche_Script>();

        for (int i = 0; i < cardsPosition.Length; i++)
        {
            cardsPosition[i].gameObject.SetActive(false);
        }
    }

    public void AddToList(CartePioche carte)
    {
        nombrePioche.Add(carte);
    }

    public void ShowTheCards(int nombre , bool endTurn)
    {
        endTurnAfterPickUp = endTurn;

        pickedUp = new List<Transform>(nombre);
        tmpPioche.Clear();

        for (int i = 0; i < nombre; i++)
        {
            Transform clone = Instantiate(cardPrefab).transform;
            clone.position = cardsPosition[i].transform.position;
            clone.rotation = cardsPosition[i].transform.rotation;

            clone.GetComponent<Card_Script>().SetCard(nombrePioche[i].carte);

            pickedUp.Add(clone);
        }

        for (int i = 0; i < nombre; i++)
        {
            cardsPosition[i].gameObject.SetActive(true);
        }
    }

    public void DrawCard(int index)
    {
        cardCount += 1;

        pickedUp[index].parent = main.transform;
        main.addCarteToMain(pickedUp[index].gameObject);
        pickedUp[index] = null;

        cardsPosition[index].gameObject.SetActive(false);

        tmpPioche.Add(nombrePioche[index]);

        if (cardCount == pioche.cardToPioche)
        {
            for (int i = 0; i < pickedUp.Count; i++)
            {
                if (pickedUp[i] != null)
                {
                    Destroy(pickedUp[i].gameObject);
                }
            }
            for (int i = 0; i < tmpPioche.Count; i++)
            {
                pioche.RemoveCarte(tmpPioche[i].carte);
            }
            for (int i = 0; i < cardsPosition.Length; i++)
            {
                cardsPosition[i].gameObject.SetActive(false);
            }
            nombrePioche.Clear();
            cardCount = 0;

            if(endTurnAfterPickUp)
            main.EndMyTurn();
        }
    }
}
