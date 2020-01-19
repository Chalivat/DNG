using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Script : MonoBehaviour
{
    public List<GameObject> carteMain = new List<GameObject>();
    public HandPlacement placement;

    public void removeCartesFromMain(GameObject carte)
    {
        carteMain.Remove(carte);
        placement.UpdatePlacement();
    }

    public void addCarteToMain(GameObject carte)
    {
        if(carte.transform.parent != transform)
        {
            carte.transform.parent = transform;
        }
        carteMain.Add(carte);
        placement.UpdatePlacement();
    }

    public List<GameObject> getMain()
    {
        return carteMain;
    }
}
