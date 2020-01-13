using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection_Script : MonoBehaviour
{
    public List<CartePioche> nombrePioche = new List<CartePioche>();
    public List<Transform> pickedUp;
    public Transform[] cardsPosition;
    Main_Script main;
    Pioche_Script pioche;
    int nombreCarte;
    int cardCount;

    private void Start()
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

    public void ShowTheCards(int nombre)
    {
        pickedUp = new List<Transform>(nombre);
        for (int i = 0; i < nombre; i++)
        {
            Transform clone = Instantiate(nombrePioche[i].carte).transform;
            clone.position = cardsPosition[i].transform.position;
            clone.rotation = cardsPosition[i].transform.rotation;

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
        main.addCarteToMain(pickedUp[index].gameObject);
        cardsPosition[index].gameObject.SetActive(false);

        Debug.Log(nombrePioche[index].index);
        pioche.RemoveCarte(nombrePioche[index].index);

        nombrePioche.RemoveAt(index);
        pickedUp.RemoveAt(index);

        if (cardCount == pioche.nombrePioche)
        {
            for (int i = 0; i < pickedUp.Count; i++)
            {
                Destroy(pickedUp[i].gameObject);
            }
            for (int i = 0; i < cardsPosition.Length; i++)
            {
                cardsPosition[i].gameObject.SetActive(false);
            }
            nombrePioche.Clear();
            cardCount = 0;
        }
    }
}
