using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigneHighlight_Script : MonoBehaviour
{
    public Transform frontBoard;
    public Transform backBoard;

    public static uint activeBoard;

    public Transform[,] allCaseFront = new Transform[5,3];
    public Transform[,] allCaseBack = new Transform[5, 3];

    public MeshRenderer[] allColonneEffectCase;
    public MeshRenderer[] allFrontEffectCases;
    public MeshRenderer[] allBackEffectCases;

    public Color darker_Color;
    public Color highlight_Color;
    public Color base_Color;

    private void Start()
    {
        Case_Script[] casesFront = frontBoard.GetComponentsInChildren<Case_Script>();
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

    public void HighlightLine(int yIndex,Color color, bool ignoreUnit,bool mirror)
    {
        if (mirror)
        {
            for (int i = 0; i < 5; i++)
            {
                if (ignoreUnit)
                {
                    allCaseFront[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
                    allCaseBack[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
                }
                else
                {
                    Case_Script case_ScriptFront = allCaseFront[i, yIndex].GetComponent<Case_Script>();
                    if (case_ScriptFront.isEmpty && !case_ScriptFront.GetComponent<Case_Effect_Manager>().isOiled)
                    {
                        allCaseFront[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
                    }
                    Case_Script case_ScriptBack = allCaseFront[i, yIndex].GetComponent<Case_Script>();
                    if (case_ScriptBack.isEmpty && !case_ScriptBack.GetComponent<Case_Effect_Manager>().isOiled)
                    {
                        allCaseBack[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
                    }
                }
            }
        }
        else
        {
            switch (activeBoard)
            {
                case 1:
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
                    break;
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        if (ignoreUnit)
                        {
                            allCaseBack[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
                        }
                        else
                        {
                            Case_Script case_Script = allCaseBack[i, yIndex].GetComponent<Case_Script>();
                            if (case_Script.isEmpty && !case_Script.GetComponent<Case_Effect_Manager>().isOiled)
                                allCaseBack[i, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
                        }
                    }
                    break;
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

    public void HighLightOccupedCase()
    {
        DarkerAllLine();

        switch (activeBoard)
        {
            case 1:
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (!allCaseFront[x, y].GetComponent<Case_Script>().isEmpty)
                            allCaseFront[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", highlight_Color);
                    }
                }
                break;
            case 2:
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (!allCaseBack[x, y].GetComponent<Case_Script>().isEmpty)
                            allCaseBack[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", highlight_Color);
                    }
                }
                break;
        }
    }

    public void HighLightEffectCaseLine()
    {
        switch(activeBoard)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    allFrontEffectCases[i].material.SetColor("_BaseColor", highlight_Color);
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    allBackEffectCases[i].material.SetColor("_BaseColor", highlight_Color);
                }
                break;
        }
    }

    public void HighLightEffectCaseColonne()
    {
        for (int i = 0; i < 10; i++)
        {
            allColonneEffectCase[i].material.SetColor("_BaseColor", highlight_Color);
        }
    }

    public void HighLightCase(int xIndex, int yIndex, uint boardNumber, Color color)
    {
        DarkerAllLine();

        if (boardNumber == 1)
        {
            allCaseFront[xIndex, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
        }
        if (boardNumber == 2)
        {
            allCaseBack[xIndex, yIndex].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
        }
    }

    public void DarkerAllLine()
    {
        foreach (MeshRenderer item in allFrontEffectCases)
        {
            item.material.SetColor("_BaseColor", darker_Color);
        }
        foreach (MeshRenderer item in allBackEffectCases)
        {
            item.material.SetColor("_BaseColor", darker_Color);
        }
        foreach (MeshRenderer item in allColonneEffectCase)
        {
            item.material.SetColor("_BaseColor", darker_Color);
        }
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                allCaseFront[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", darker_Color);
                allCaseBack[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", darker_Color);
            }
        }
    }

    public void ClearAllCase()
    {
        foreach (MeshRenderer item in allFrontEffectCases)
        {
            item.material.SetColor("_BaseColor", base_Color);
        }
        foreach (MeshRenderer item in allBackEffectCases)
        {
            item.material.SetColor("_BaseColor", base_Color);
        }
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                allCaseFront[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", base_Color);
                allCaseBack[x, y].GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", base_Color);
            }
        }
    }

    public static void SetActiveBoard(uint boardNumber)
    {
        activeBoard = boardNumber;
    }

    public static void EchangeActiveBoard()
    {
        uint i = activeBoard;
        if(i == 1)
        {
            activeBoard = 2;
        }
        else
        {
            activeBoard = 1;
        }
    }
}
