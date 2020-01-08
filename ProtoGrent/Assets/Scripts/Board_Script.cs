using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board_Script : MonoBehaviour
{
    public Case_Script[,] allCase = new Case_Script[5,3];

    private void OnEnable()
    {
        Case_Script[] cases = GetComponentsInChildren<Case_Script>();
        foreach (Case_Script item in cases)
        {
            allCase[(int)item.pos.x,(int)item.pos.y] = item;
        }
    }
}