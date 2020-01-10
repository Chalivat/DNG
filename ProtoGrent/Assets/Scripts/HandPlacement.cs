using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlacement : MonoBehaviour
{
    public Transform[] handCard;

    public Transform pivot;

    public float angle;

    private void Start()
    {
        UpdatePlacement();
    }

    public void UpdatePlacement()
    {
        int x = 0;
        for (int i = -handCard.Length / 2; i < handCard.Length / 2; i++)
        {
            handCard[x].RotateAround(pivot.position, pivot.forward, angle / handCard.Length * i);
            x++;
        }
    }
}
