using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigneHighlight_Script : MonoBehaviour
{
    public Transform backBoard;

    public Transform[,] allCaseFront = new Transform[5,3];
    public Transform[,] allCaseBack = new Transform[5, 3];

    public MeshRenderer[] effectCases;

    public Color highlight_Color;
    public Color base_Color;

    private void Start()
    {
        Case_Script[] casesFront = GetComponentsInChildren<Case_Script>();
        Case_Script[] casesBack = backBoard.GetComponentsInChildren<Case_Script>();

        foreach (Case_Script item in casesFront)
        {
            allCaseFront[(int)item.pos.x, (int)item.pos.y] = item.transform;
        }
        foreach (Case_Script item in casesBack)
        {
            allCaseBack[(int)item.pos.x, (int)item.pos.y] = item.transform;
        }

        base_Color = casesFront[0].transform.GetChild(0).GetComponentInChildren<MeshRenderer>().material.GetColor("_BaseColor");
    }

    public void HighlightLine(int yIndex,Color color, bool ignoreUnit)
    {
        for (int i = 0; i < 5; i++)
        {
            if (ignoreUnit)
            {
                allCaseFront[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }
            else
            {
                Case_Script case_Script = allCaseFront[i, yIndex].GetComponent<Case_Script>();
                if (case_Script.isEmpty && !case_Script.GetComponent<Case_Effect_Manager>().isOiled)
                    allCaseFront[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }
        }
    }

    public void HighLightColonne(int xIndex, Color color)
    {
        for (int i = 0; i < 3; i++)
        {
            allCaseFront[xIndex, i].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            allCaseBack[xIndex, i].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
        }
    }

    public void HighLightOccupedCase(Color color)
    {
        for (int x   = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (!allCaseFront[x, y].GetComponent<Case_Script>().isEmpty)
                    allCaseFront[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
            }
        }
    }

    public void HighLightEffectCase(Color color)
    {
        foreach (MeshRenderer item in effectCases)
        {
            item.material.SetColor("_BaseColor", color);
        }
    }

    public void ClearAllLine()
    {
        HighlightLine(0, base_Color,true);
        HighlightLine(1, base_Color, true);
        HighlightLine(2, base_Color, true);
    }

    public void ClearAllColonne()
    {
        HighLightColonne(0, base_Color);
        HighLightColonne(1, base_Color);
        HighLightColonne(2, base_Color);
        HighLightColonne(3, base_Color);
        HighLightColonne(4, base_Color);
    }
}
