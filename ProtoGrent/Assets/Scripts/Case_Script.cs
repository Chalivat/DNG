using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Script : MonoBehaviour
{
    public Vector2 pos;

    public bool Check(int type)
    {
        if(pos.y == type)
        {
            return true;
        }
        return false;
    }

    public void UpdateBoard(Vector2 pos)
    {
        GetComponentInParent<Board_Script>().allCase[(int)pos.x,(int)pos.y] = this;
    }
}
