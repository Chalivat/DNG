using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board_Script : MonoBehaviour
{
    public Case_Script[,] allCase = new Case_Script[5,3];
    public Case_Unit_Manager[,] allUnitCase = new Case_Unit_Manager[5, 3];
    public Case_Effect_Manager[,] allEffect = new Case_Effect_Manager[5, 3];
    public bool[,] allEncouragement = new bool[5, 3];

    public delegate void BoardUpdate();
    public static BoardUpdate boardUpdate;

    public int point;

    private void Start()
    {
        UpdateBoardCases();
        Case_Script.cardPlaced += CountPoint;
    }

    public void UpdateBoardCases()
    {
        Case_Script[] cases = GetComponentsInChildren<Case_Script>();
        Case_Effect_Manager[] effect = GetComponentsInChildren<Case_Effect_Manager>();
        Case_Unit_Manager[] unit = GetComponentsInChildren<Case_Unit_Manager>();

        foreach (Case_Script item in cases)
        {
            allCase[(int)item.pos.x, (int)item.pos.y] = item;
        }
        foreach (Case_Effect_Manager item in effect)
        {
            allEffect[(int)item.pos.x, (int)item.pos.y] = item;
            allEncouragement[(int)item.pos.x, (int)item.pos.y] = item.isEncouraged;
        }
        foreach (Case_Unit_Manager item in unit)
        {
            allUnitCase[(int)item.pos.x, (int)item.pos.y] = item;
        }
    }

    public List<Card> GetCardPlaced()
    {
        List<Card> allCardPlaced = new List<Card>();

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if(allCase[x,y].card != null)
                allCardPlaced.Add(allCase[x, y].card);
            }
        }

        return allCardPlaced;
    }

    public Card[,] GetAllCard()
    {
        Card[,] allCard = new Card[5,3];

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                    allCard[x,y] = allCase[x, y].card;
            }
        }

        return allCard;
    }

    public Transform[,] GetAllUnitsParent()
    {
        Transform[,] allUnitsParent = new Transform[5, 3];

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                allUnitsParent[x, y] = allUnitCase[x, y].GetUnitsParent();
            }
        }

        return allUnitsParent;
    }

    public void SetAllUnitPosition(Transform[,] allUnit)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                allUnitCase[x, y].SetUnitOnCase(null);
                if (allUnit[x, y] != null)
                {
                    allUnitCase[x, y].SetUnitOnCase(allUnit[x, y]);
                    allUnit[x, y].SetParent(allCase[x, y].transform);
                    allUnit[x, y].localPosition = Vector3.zero;
                }
            }
        }
    }

    public void SetCardOnBoard(Card[,] allCard)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                allCase[x, y].SetCard(allCard[x, y]);
                if (allCard[x, y] != null)
                {
                    allCase[x, y].isEmpty = false;
                }
                else
                {
                    allCase[x, y].isEmpty = true;
                }
            }
        }
    }

    public void SetEncrougement(bool[,] allEncouragement)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                allEffect[x, y].isEncouraged = allEncouragement[x, y];
            }
        }
    }

    public void ClearBoard()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (allCase[x, y] != null)
                {
                    allCase[x, y].SetCard(null);
                    allCase[x, y].isEmpty = true;
                    allCase[x, y].power = 0;
                }

                if (allEffect[x, y] != null)
                {
                    allEffect[x, y].isEncouraged = false;
                    allEffect[x, y].isWatered = false;
                    allEffect[x, y].isFired = false;
                    allEffect[x, y].isOiled = false;

                    allEffect[x, y].ClearAllImage();
                }

                if(allUnitCase[x,y] != null)
                {
                    allUnitCase[x, y].DestroyUnitOnCase();
                }
            }
        }
        CountPoint();
    }

    public void CountPoint()
    {
        point = 0;

        foreach (Case_Script item in allCase)
        {
            item.CountPointOnCase();
            point += item.power;
        }
        boardUpdate();
    }
}