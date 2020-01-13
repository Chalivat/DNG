using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Script : MonoBehaviour
{
    public List<GameObject> carteMain = new List<GameObject>();
    public HandPlacement placement;
    
    void Start()
    {
        
    }
    
    void Update()
    {

    }

    public void removeCartesFromMain(GameObject carte)
    {
        carteMain.Remove(carte);
        placement.UpdatePlacement();
    }

    public void addCarteToMain(GameObject carte)
    {
        carte.GetComponent<ChangeEvent_Script>().isOnMain = true;
        carteMain.Add(carte);
        placement.UpdatePlacement();
        Debug.Log("carte added");
    }

    public List<GameObject> getMain()
    {
        return carteMain;
    }
}
