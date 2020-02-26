using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlacement : MonoBehaviour
{
    public Main_Script Main_Script;

    public List<GameObject> tmp_Main;

    public float xOffSetMain = -7f;
    public float xOffSet = -.5f;

    public int MinScreeWidth;

    private void Start()
    {
        UpdatePlacement();
    }

    public void UpdatePlacement()
    {
        Debug.Log("Update");

        tmp_Main = new List<GameObject>();
        tmp_Main.Clear();
        tmp_Main = Main_Script.getMain();
        if (tmp_Main.Count == 0)
            return;

        int width = Screen.width;
        int padding = width / tmp_Main.Count;

        float test = tmp_Main.Count;

        int x = 0;
        for (float i = -(test / 2) ; i < (test / 2) ; i++)
        {
            //tmp_Main[x].transform.localPosition = Vector3.zero;

            tmp_Main[x].transform.localEulerAngles = new Vector3(90, 0, 0);

            Card_Script card_Script = tmp_Main[x].GetComponent<Card_Script>();

            if (!Main_Script.mainIsOpen)
            {
                Vector3 pos = tmp_Main[x].transform.localPosition;
                if (width >= MinScreeWidth)
                {
                    pos = new Vector3(padding * i * xOffSetMain, -.025f * x, 0);
                    pos = new Vector3(pos.x, pos.y, 0);

                    LerpManager lerpCard = new LerpManager(tmp_Main[x].transform.localPosition, pos, tmp_Main[x].transform, .75f, true, true, LerpCurve.Curve.cardExpendCustomCurve);
                    lerpCard.StartLerp();
                }
                else
                {
                    tmp_Main[x].transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                LerpManager lerpCard = new LerpManager(tmp_Main[x].transform.localPosition, tmp_Main[x].transform.localPosition += new Vector3(xOffSet * i, 0, 0), tmp_Main[x].transform, .75f, true,true, LerpCurve.Curve.cardExpendCustomCurve);
                lerpCard.StartLerp();
            }

            card_Script.posInMain = tmp_Main[x].transform.localPosition;
            card_Script.rotInMain = tmp_Main[x].transform.localEulerAngles;

            x++;
        }
    }
}
