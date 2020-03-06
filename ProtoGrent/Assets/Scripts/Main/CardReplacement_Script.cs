using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReplacement_Script : MonoBehaviour
{
    public Main_Script main;

    public int maxCardToReplace = 3;

    private void Start()
    {
        GameManager.newgame += StartReplace;
    }

    public void StartReplace()
    {
        Debug.Log("START");
        main.ShowMain(true);
    }

    void EndReplace()
    {

    }
}