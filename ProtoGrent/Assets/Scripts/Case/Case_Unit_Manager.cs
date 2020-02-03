using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Unit_Manager : MonoBehaviour
{
    public Vector2 pos;

    public Transform unitsParent;

    private void Start()
    {
        pos = GetComponent<Case_Script>().pos;
    }

    public void SetUnitOnCase(Transform parent)
    {
        unitsParent = parent;
    }

    public void DestroyUnitOnCase()
    {
        if (unitsParent != null)
        {
            Debug.Log("DestroyUNIT");
            Destroy(unitsParent.gameObject);
        }
    }

    public Transform GetUnitsParent()
    {
        return unitsParent;
    }
}
