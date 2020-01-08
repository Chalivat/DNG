using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Script : MonoBehaviour
{
    List<GameObject> carteMain = new List<GameObject>();
    

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            print(carteMain);
        }
    }
}
