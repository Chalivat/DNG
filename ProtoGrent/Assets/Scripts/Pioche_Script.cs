using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioche_Script : MonoBehaviour
{
    public List<GameObject> carte = new List<GameObject>();
    List<int> random = new List<int>();
    public CardSelection_Script selection;
    public int nombrePioche;
    int nombreCarte;
    int indexCartes;

    void Start()
    {

    }

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
    }

    void ShowCarte(int nombre)
    {
        for (int i = 0; i < nombre; i++)
        {
            int rnd = Random.Range(0, carte.Count - 1);

            if (random.IndexOf(rnd) == -1)
            {
                random.Add(rnd);
                selection.AddToList(new CartePioche(carte[rnd], rnd));

                if (i == nombre - 1)
                {
                    selection.ShowTheCards(nombreCarte);
                }
            }
            else
            {
                print("Mangus Tes morus");
                ShowCarte(nombreCarte);
            }
        }
    }

    public void RemoveCarte(int index)
    {
        carte.RemoveAt(index);
        random.Clear();
    }
}

[System.Serializable]
public class CartePioche
{
    public GameObject carte;
    public int index;

    public CartePioche(GameObject carte, int index)
    {
        this.carte = carte;
        this.index = index;
    }
}
