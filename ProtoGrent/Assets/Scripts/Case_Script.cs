using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Script : MonoBehaviour
{
    public Vector2 pos;

    public bool isEmpty = true;

    public bool Check(int type)
    {
        if(pos.y == type)
        {
            return true;
        }
        return false;
    }

    public void PlacerCarte()
    {

        isEmpty = true;
        //set scriptable object value

        UpdateBoard();
    }

    void UpdateBoard()
    {
        GetComponentInParent<Board_Script>().allCase[(int)pos.x,(int)pos.y] = this;
    }
}