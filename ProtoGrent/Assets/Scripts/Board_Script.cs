using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board_Script : MonoBehaviour
{
    public Case_Script[,] allCase = new Case_Script[5,3];

    public int point;

    private void OnEnable()
    {
        UpdateBoardCases();
    }

    public void UpdateBoardCases()
    {
        Case_Script[] cases = GetComponentsInChildren<Case_Script>();
        foreach (Case_Script item in cases)
        {
            allCase[(int)item.pos.x, (int)item.pos.y] = item;
        }

        CountPoint();
    }

    void CountPoint()
    {
        point = 0;

        foreach (Case_Script item in allCase)
        {
            Card currentCard = item.card;

            if(currentCard != null)
            {
                point += currentCard.damage;
            }
        }
    }
}