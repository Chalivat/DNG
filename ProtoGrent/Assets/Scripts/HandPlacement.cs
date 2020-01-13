using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlacement : MonoBehaviour
{
    public Main_Script Main_Script;

    public List<GameObject> tmp_Main;

    public Transform pivot;

    public float angle;
    public float xOffSet;

    private void Start()
    {
        UpdatePlacement();
    }

    public void UpdatePlacement()
    {
        tmp_Main = new List<GameObject>();
        tmp_Main.Clear();
        tmp_Main = Main_Script.getMain();

        int x = 0;
        for (int i = -tmp_Main.Count / 2; i < tmp_Main.Count / 2; i++)
        {
                tmp_Main[x].transform.localPosition = Vector3.zero;
                tmp_Main[x].transform.localEulerAngles = new Vector3(60, 0, 0);

                Card_Script card_Script = tmp_Main[x].GetComponent<Card_Script>();

                tmp_Main[x].transform.RotateAround(pivot.position, pivot.forward, angle / tmp_Main.Count * i);
                tmp_Main[x].transform.localPosition += new Vector3(xOffSet * i, 0, 0);
                tmp_Main[x].transform.Translate(transform.forward * .05f * x, Space.Self);

                card_Script.posInMain = tmp_Main[x].transform.localPosition;
                card_Script.rotInMain = tmp_Main[x].transform.localEulerAngles;

                x++;
        }
    }
}
