using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlacement : MonoBehaviour
{
    public Main_Script Main_Script;

    public List<GameObject> tmp_Main;

    public Transform pivot;

    public float angle;

    public float xOffSetMain = -7f;
    public float xOffSet = -.5f;

    private void Start()
    {
        UpdatePlacement();
    }

    public void UpdatePlacement()
    {
        int width = Screen.width;

        tmp_Main = new List<GameObject>();
        tmp_Main.Clear();
        tmp_Main = Main_Script.getMain();

        float test = tmp_Main.Count;

        int x = 0;
        for (float i = -(test / 2) ; i < (test / 2) ; i++)
        {
            tmp_Main[x].transform.localPosition = Vector3.zero;
            tmp_Main[x].transform.localEulerAngles = new Vector3(90, 0, 0);

            Card_Script card_Script = tmp_Main[x].GetComponent<Card_Script>();

            if (!Main_Script.mainIsOpen)
            {
                tmp_Main[x].transform.localPosition += new Vector3(xOffSetMain / tmp_Main.Count * i, -.025f * x, 0);
            }
            else
            {
                tmp_Main[x].transform.localPosition += new Vector3(xOffSet * i, 0 , 0);
            }

            card_Script.posInMain = tmp_Main[x].transform.localPosition;
            card_Script.rotInMain = tmp_Main[x].transform.localEulerAngles;

            x++;
        }
    }
}
