using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Script : MonoBehaviour
{
    public List<GameObject> carteMain = new List<GameObject>();
    

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            print(carteMain);
        }
    }
}
