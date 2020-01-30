using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Case_Effect_Manager : MonoBehaviour
{
    public Vector2 pos;

    public bool isFired = false;
    public bool isWatered = false;
    public bool isOiled = false;
    public bool isEncouraged = false;

    private void Start()
    {
        pos = GetComponent<Case_Script>().pos;
    }
}
