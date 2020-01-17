using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisual : MonoBehaviour
{
    public GameObject Card;

    public void DestroyCard()
    {
        Destroy(transform.parent.gameObject);
    }
}
