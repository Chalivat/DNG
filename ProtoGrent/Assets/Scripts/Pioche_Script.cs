using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioche_Script : MonoBehaviour
{
    public List<Card> carte = new List<Card>();
    public List<int> random = new List<int>();
    public CardSelection_Script selection;
    public int nombrePioche;
    int nombreCarte;
    int indexCartes;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CombienDePioche(nombrePioche);
            ShowCarte(nombreCarte);
        }
    }

    void CombienDePioche(int nombre)
    {
        if(nombrePioche > 3)
        {
            nombreCarte = 5;
        }
        else
        {
            nombreCarte = 3;
        }

        if(carte.Count < nombrePioche)
        {
            nombrePioche = carte.Count;
        }
    }

    void ShowCarte(int nombre)
    {
        if(carte.Count < nombre)
        {
            nombre = carte.Count;
        }
        random.Clear();
            while (random.Count < nombre)
            {
                int rnd = Random.Range(0, carte.Count);
                if (!random.Contains(rnd))
                {
                    random.Add(rnd);
                }
            }

            for (int t = 0; t < random.Count; t++)
            {
                selection.AddToList(new CartePioche(carte[random[t]], random[t]));
            }

            selection.ShowTheCards(nombre);
    }

    public void RemoveCarte(Card cardToRemove)
    {
        carte.Remove(cardToRemove);
    }

    public List<Card> SimplyAddCardsToPlayer(int nombre)
    {
        List<Card> allCard = new List<Card>();
        for (int i = 0; i < nombre; i++)
        {
            int rnd = Random.Range(0, carte.Count);
            allCard.Add(carte[rnd]);
            carte.RemoveAt(rnd);
        }
        return allCard;
    }
}

[System.Serializable]
public class CartePioche
{
    public Card carte;
    public int index;

    public CartePioche(Card carte, int index)
    {
        this.carte = carte;
        this.index = index;
    }
}
