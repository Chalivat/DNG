using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoriqueDisplay : MonoBehaviour
{
    public PlayerCoup[] playerCoups;
    public GameObject[] allSlot;

    public GameObject slotPrefab;

    public int index = 0;

    private void Start()
    {
        playerCoups = new PlayerCoup[8];
        allSlot = new GameObject[8];
    }

    public void AddCoup(PlayerCoup coup)
    {
        if (index > 7)
        {
            index = 0;
        }

        if (allSlot[7] != null)
        {
            Destroy(allSlot[0+index].gameObject);
        }

        playerCoups[index] = coup;
        HistoriqueSlot newSlot = Instantiate(slotPrefab, transform).GetComponent<HistoriqueSlot>();
        newSlot.coup = coup;
        newSlot.UpdateSlot();

        PuchSlotUp();

        allSlot[index] = newSlot.gameObject;

        newSlot.GetComponent<RectTransform>().localPosition = new Vector3(0f, -175f, 0f);
        index++;
    }

    void PuchSlotUp()
    {
        for (int i = 0; i < playerCoups.Length; i++)
        {
            if(allSlot[i] != null)
            allSlot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, 50f, 0);
        }
    }

    public void ClearHistorique()
    {
        for (int i = 0; i < playerCoups.Length; i++)
        {
            playerCoups[i] = null;
            Destroy(allSlot[i]);
        }
    }
}
