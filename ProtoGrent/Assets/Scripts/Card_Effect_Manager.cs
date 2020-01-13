using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Card_Effect_Manager : MonoBehaviour
{

    public void ReceiveEffect(string effectName)
    {
        Debug.DrawRay(transform.position,transform.up,Color.blue);
    }
}
