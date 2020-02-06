using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Board_Script Front_board, Back_board;

    public GameObject[] particleEffect;

    public Vector3[] particleEffectPos_Front;

    public Vector3[] particleEffectPos_Colonne;

    Case_Script[,] allCase_Front = new Case_Script[5, 3];
    Case_Script[,] allCase_Back = new Case_Script[5, 3];

    List<EffectClass> effectBuffer = new List<EffectClass>();

    public delegate void PiocheEvent(int nombre, bool endTurn);
    public static PiocheEvent PiocheEffect;

    public delegate void DefausseEvent(int nombre);
    public static DefausseEvent DefausseEffect;

    private void Start()
    {
        Case_Script.triggerEffect += EventTriggered;
        GameManager.newTurn += PlayEffectBuffer;

        allCase_Front = new Case_Script[3, 5];
        allCase_Front = Front_board.allCase;
        allCase_Back = new Case_Script[3, 5];
        allCase_Back = Back_board.allCase;
    }

    void EventTriggered(EffectClass effect)
    {
        switch(effect.effectType)
        {
            case Card.EffectType.Fire:
                if (effect.isColonne)
                {
                    FireEffectColonne((int)effect.pos.x);

                    SpawnParticle(particleEffect[4], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    FireEffect((int)effect.pos.y);

                    Vector3 SpawnPos = particleEffectPos_Front[(int)effect.pos.y];

                    SpawnParticle(particleEffect[0], SpawnPos);
                    SpawnParticle(particleEffect[0], new Vector3(SpawnPos.x,SpawnPos.y,Mathf.Abs(SpawnPos.z)));
                }
                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;

                effect.case_Script.EndMyTurn();
                break;

            case Card.EffectType.Water:
                if (effect.isColonne)
                {
                    WaterEffectColonne((int)effect.pos.x);

                    SpawnParticle(particleEffect[5], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    WaterEffect((int)effect.pos.y);

                    Vector3 SpawnPos = particleEffectPos_Front[(int)effect.pos.y];

                    SpawnParticle(particleEffect[1], SpawnPos);
                    SpawnParticle(particleEffect[1], new Vector3(SpawnPos.x, SpawnPos.y, Mathf.Abs(SpawnPos.z)));
                }
                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;

                effect.case_Script.EndMyTurn();
                break;

            case Card.EffectType.Oil:
                if (effect.isColonne)
                {
                    OilEffectColonne((int)effect.pos.x);

                    SpawnParticle(particleEffect[6], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    OilEffect((int)effect.pos.y);

                    Vector3 SpawnPos = particleEffectPos_Front[(int)effect.pos.y];

                    SpawnParticle(particleEffect[2], SpawnPos);
                    SpawnParticle(particleEffect[2], new Vector3(SpawnPos.x, SpawnPos.y, Mathf.Abs(SpawnPos.z)));
                }
                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;

                effect.case_Script.EndMyTurn();
                break;

            case Card.EffectType.Encouragement:
                EncouragementEffect((int)effect.pos.y,effect.case_Script.boardNumber,effect.pos);

                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;

                effect.case_Script.EndMyTurn();

                break;

            case Card.EffectType.Defausse:
                DefausseEffect(effect.numCardIfNeeded);
                break;

            case Card.EffectType.Pioche:
                PiocheEffect(effect.numCardIfNeeded,true);
                break;

            case Card.EffectType.Rafle:
                if (effect.isColonne)
                {
                    int piocheNumber = GetNonEmptyCaseColonne((int)effect.pos.x);
                    if (piocheNumber > 0)
                        PiocheEffect(piocheNumber, true);
                    else
                        effect.case_Script.EndMyTurn();
                }
                else
                {
                    int piocheNumber = GetNonEmptyCase((int)effect.pos.y);
                    if (piocheNumber > 0)
                        PiocheEffect(piocheNumber, true);
                    else
                        effect.case_Script.EndMyTurn();
                }

                effectBuffer.Add(effect);

                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;
                break;

            case Card.EffectType.Zap:
                ZapEffect(effect.case_Script);
                break;

            case Card.EffectType.Clean:
                if(effect.isColonne)
                {
                    CleanEffectColonne((int)effect.pos.x);

                    SpawnParticle(particleEffect[8], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    CleanEffect((int)effect.pos.y);

                    Vector3 SpawnPos = particleEffectPos_Front[(int)effect.pos.y];

                    SpawnParticle(particleEffect[7], SpawnPos);
                    SpawnParticle(particleEffect[7], new Vector3(SpawnPos.x, SpawnPos.y, Mathf.Abs(SpawnPos.z)));
                }
                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;

                effect.case_Script.EndMyTurn();
                break;
        }
    }

    void SpawnParticle(GameObject prefab, Vector3 pos)
    {
        Transform particle = Instantiate(prefab).transform;
        particle.position = pos;

        Destroy(particle.gameObject, 5f);
    }

    int GetNonEmptyCase(int ligne)
    {
        int caseNonEmpty = 0;

        for (int i = 0; i < 5; i++)
        {
            if (!allCase_Front[i, ligne].isEmpty)
                caseNonEmpty++;
        }

        return caseNonEmpty;
    }

    int GetNonEmptyCaseColonne(int colonne)
    {
        int caseNonEmpty = 0;

        for (int i = 0; i < 3; i++)
        {
            if (!allCase_Front[colonne, i].isEmpty)
                caseNonEmpty++;
        }

        return caseNonEmpty;
    }

    void ZapEffect(Case_Script case_Script)
    {
        case_Script.GetComponent<Case_Unit_Manager>().DestroyUnitOnCase();

        case_Script.SetCard(null);
        case_Script.isEmpty = true;

        PiocheEffect(1,true);
    }

    void EncouragementEffect(int posY, uint boardNumber,Vector2 pos)
    {
        if (boardNumber == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                Case_Effect_Manager case_Effect_Front = allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
                case_Effect_Front.isEncouraged = true;
                case_Effect_Front.AddImage(case_Effect_Front.modifierSprite[3]);

                SpawnParticle(particleEffect[3], particleEffectPos_Front[(int)pos.y]);
            }
        }
        else if(boardNumber == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                Case_Effect_Manager case_Effect_Back = allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
                case_Effect_Back.isEncouraged = true;
                case_Effect_Back.AddImage(case_Effect_Back.modifierSprite[3]);

                Vector3 SpawnPos = particleEffectPos_Front[(int)pos.y];
                SpawnParticle(particleEffect[3], new Vector3(SpawnPos.x, SpawnPos.y, Mathf.Abs(SpawnPos.z)));
            }
        }
    }

    void CleanEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            Case_Effect_Manager case_Effect_Front = allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Front.isWatered = false;
            case_Effect_Front.isFired = false;
            case_Effect_Front.isOiled = false;
            case_Effect_Front.ClearImage();

            Case_Effect_Manager case_Effect_Back = allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Back.isWatered = false;
            case_Effect_Back.isFired = false;
            case_Effect_Back.isOiled = false;
            case_Effect_Back.ClearImage();
        }
    }

    void CleanEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            Case_Effect_Manager case_Effect_Front = allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Front.isWatered = false;
            case_Effect_Front.isFired = false;
            case_Effect_Front.isOiled = false;
            case_Effect_Front.ClearImage();

            Case_Effect_Manager case_Effect_Back = allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Back.isWatered = false;
            case_Effect_Back.isFired = false;
            case_Effect_Back.isOiled = false;
            case_Effect_Back.ClearImage();
        }
    }

    void WaterEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            Case_Effect_Manager case_Effect_Front = allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Front.isWatered = true;
            case_Effect_Front.AddImage(case_Effect_Front.modifierSprite[1]);

            Case_Effect_Manager case_Effect_Back = allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Back.isWatered = true;
            case_Effect_Back.AddImage(case_Effect_Back.modifierSprite[1]);
        }
    }

    void WaterEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            Case_Effect_Manager case_Effect_Front = allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Front.isWatered = true;
            case_Effect_Front.AddImage(case_Effect_Front.modifierSprite[1]);

            Case_Effect_Manager case_Effect_Back = allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>();
            case_Effect_Back.isWatered = true;
            case_Effect_Back.AddImage(case_Effect_Back.modifierSprite[1]);
        }
    }

    void FireEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            Case_Effect_Manager frontCase = allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
            Case_Effect_Manager backCase = allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>();

            frontCase.isFired = true;
            frontCase.CheckExplose();
            frontCase.AddImage(frontCase.modifierSprite[0]);

            backCase.isFired = true;
            backCase.CheckExplose();
            backCase.AddImage(backCase.modifierSprite[0]);
        }
    }

    void FireEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            Case_Effect_Manager frontCase = allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>();
            Case_Effect_Manager backCase = allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>();

            frontCase.isFired = true;
            frontCase.CheckExplose();
            frontCase.AddImage(frontCase.modifierSprite[0]);

            backCase.isFired = true;
            backCase.CheckExplose();
            backCase.AddImage(backCase.modifierSprite[0]);
        }
    }

    void OilEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            Case_Effect_Manager frontCase = allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>();
            Case_Effect_Manager backCase = allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>();

            frontCase.isOiled = true;
            frontCase.CheckExplose();
            frontCase.AddImage(frontCase.modifierSprite[2]);

            backCase.isOiled = true;
            backCase.CheckExplose();
            backCase.AddImage(backCase.modifierSprite[2]);
        }
    }

    void OilEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            Case_Effect_Manager frontCase = allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>();
            Case_Effect_Manager backCase = allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>();

            frontCase.isOiled = true;
            frontCase.CheckExplose();
            frontCase.AddImage(frontCase.modifierSprite[2]);

            backCase.isOiled = true;
            backCase.CheckExplose();
            backCase.AddImage(backCase.modifierSprite[2]);
        }
    }

    void PlayEffectBuffer()
    {
        foreach (EffectClass item in effectBuffer)
        {
            if (item.isColonne)
            {
                int piocheNumber = GetNonEmptyCaseColonne((int)item.pos.x);
                if (piocheNumber > 0)
                    PiocheEffect(piocheNumber, false);
                else
                    item.case_Script.EndMyTurn();
            }
            else
            {
                int piocheNumber = GetNonEmptyCase((int)item.pos.y);
                if (piocheNumber > 0)
                    PiocheEffect(piocheNumber, false);
                else
                    item.case_Script.EndMyTurn();
            }
        }
        effectBuffer.Clear();
    }

    public int GetEffectBufferLenght()
    {
        return effectBuffer.Count;
    }
}
