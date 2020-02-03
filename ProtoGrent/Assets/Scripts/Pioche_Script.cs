using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioche_Script : MonoBehaviour
{
    public List<Card> carte = new List<Card>();
    public List<int> random = new List<int>();
    public CardSelection_Script selection;

    public int cardToPioche;
    public int cardToShow;

    private void Start()
    {
        EffectManager.PiocheEffect += ShowCarte;
    }

    void ShowCarte(int nombre, bool endTurn)
    {
        cardToPioche = nombre;
        if (nombre > 3)
        {
            cardToShow = 5;
        }
        else
        {
            cardToShow = 3;
        }
        if (carte.Count < nombre)
        {
            nombre = carte.Count;
        }

        random.Clear();

            while (random.Count < cardToShow)
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

            selection.ShowTheCards(cardToShow, endTurn);
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
