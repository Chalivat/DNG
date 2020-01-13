using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Effect_Receiver : MonoBehaviour
{
    public bool isLigne;
    public int length;
    public int index;
    

    public Board_Script board;

    void Start()
    {
        if (isLigne) length = 5;
        else length = 3;

        board = FindObjectOfType<Board_Script>();

        foreach (Case_Script item in board.allCase)
        {
            item.gameObject.AddComponent<Card_Effect_Manager>();
        }
    }
    
    void Update()
    {
    
    }

    public void ApplyEffect(string effectName)
    {
        if (isLigne)
        {
            for (int i = 0; i < length; i++)
            {
                board.allCase[i, index].gameObject.GetComponent<Card_Effect_Manager>().ReceiveEffect(effectName);
            }
        }

    else
            for (int i = 0; i < length; i++)
            {
                board.allCase[index, i].gameObject.GetComponent<Card_Effect_Manager>().ReceiveEffect(effectName);
            }
    }

    public void ReceiveEffect(string effectName)
    {
        ApplyEffect(effectName);
    }
}
