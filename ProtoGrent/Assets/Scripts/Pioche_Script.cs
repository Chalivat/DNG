using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioche_Script : MonoBehaviour
{
    public List<string> carte = new List<string>();
    public int nombrePioche;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowCarte(nombrePioche);
        }
    }

    void ShowCarte(int nombre)
    {
        for (int i = 0; i < nombre; i++)
        {
            int rnd = Random.Range(0, carte.Count - 1);
            print(carte[rnd]);
            carte.RemoveAt(rnd);
        }
    }
}
