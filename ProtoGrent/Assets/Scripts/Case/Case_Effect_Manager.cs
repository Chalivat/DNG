using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Case_Effect_Manager : MonoBehaviour
{
    public Vector2 pos;

    public GameObject BoomEffect;

    public bool isFired = false;
    public bool isWatered = false;
    public bool isOiled = false;
    public bool isEncouraged = false;

    private void Start()
    {
        pos = GetComponent<Case_Script>().pos;
    }

    public void CheckExplose()
    {
        if(isFired && isOiled)
        {
            GetComponent<Case_Script>().SetCard(null);

            Case_Unit_Manager unitManager = GetComponent<Case_Unit_Manager>();
            if (unitManager != null)
            {
                Transform unit = unitManager.unitsParent;
                if (unit != null)
                    unitManager.DestroyUnitOnCase();
            }
            GameObject effect = Instantiate(BoomEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2);
        }
    }
}
