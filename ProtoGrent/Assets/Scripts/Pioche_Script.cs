using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioche_Script : MonoBehaviour
{
    public List<GameObject> carte = new List<GameObject>();
    public CardSelection_Script selection;
    public int nombrePioche;
    int nombreCarte;

    void Start()
    {
        selection = GetComponent<CardSelection_Script>();
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
            print(carte[rnd]);
            selection.nombrePioche.Add(carte[rnd]);
            carte.RemoveAt(rnd);
        }
    }
}
