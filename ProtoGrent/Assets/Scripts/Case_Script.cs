using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Script : MonoBehaviour
{
    public Vector2 pos;
    public Card card;

    public bool isEmpty = true;

    public bool Check(int type)
    {
        if(pos.y == type)
        {
            return true;
        }
        return false;
    }

    public void PlacerCarte(Card newCard, GameObject cardObject)
    {
        Debug.Log("CARTE PLACEE");
        isEmpty = false;
        card = newCard;

        UpdateBoard();

        Destroy(cardObject);
    }

    void UpdateBoard()
    {
        GetComponentInParent<Board_Script>().UpdateBoardCases();
    }
}