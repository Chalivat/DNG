using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigneHighlight_Script : MonoBehaviour
{
    public Transform ligne_Front;
    public List<Transform> allCase_Front;

    public Transform ligne_Distance;
    public List<Transform> allCase_Distance;

    public Transform ligne_Artillerie;
    public List<Transform> allCase_Artillerie;

    public Transform ligne_Effet;
    public List<Transform> allCase_Effet;

    public Color highlight_Color;
    public Color base_Color;

    private void Start()
    {
        for (int i = 0; i < ligne_Front.childCount; i++)
        {
            allCase_Front.Add(ligne_Front.GetChild(i));
        }
        for (int i = 0; i < ligne_Distance.childCount; i++)
        {
            allCase_Distance.Add(ligne_Distance.GetChild(i));
        }
        for (int i = 0; i < ligne_Artillerie.childCount; i++)
        {
            allCase_Artillerie.Add(ligne_Artillerie.GetChild(i));
        }
        for (int i = 0; i < ligne_Effet.childCount; i++)
        {
            allCase_Effet.Add(ligne_Effet.GetChild(i));
        }

        base_Color = allCase_Front[0].GetComponentInChildren<MeshRenderer>().material.GetColor("_BaseColor");
    }

    public void HighlightLine(int yIndex,Color color)
    {
        switch(yIndex)
        {
            case 0:
                foreach (Transform caseCarte in allCase_Front)
                {
                    if (caseCarte.GetComponent<Case_Script>().isEmpty)
                    {
                        caseCarte.GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", color);
                    }
                }
                break;
            case 1:
                foreach (Transform caseCarte in allCase_Distance)
                {
                    if (caseCarte.GetComponent<Case_Script>().isEmpty)
                    {
                        caseCarte.GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", color);
                    }
                }
                break;
            case 2:
                foreach (Transform caseCarte in allCase_Artillerie)
                {
                    if (caseCarte.GetComponent<Case_Script>().isEmpty)
                    {
                        caseCarte.GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", color);
                    }
                }
                break;
            default:
                Debug.Log("pas de ligne : " + yIndex);
                break;
        }
    }
}
