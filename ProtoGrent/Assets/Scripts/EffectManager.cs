using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Board_Script Front_board, Back_board;

    public GameObject[] particleEffect;

    public Vector3[] particleEffectPos_Front;
    public Vector3[] particleEffectPos_Back;

    public Vector3[] particleEffectPos_Colonne;

    Case_Script[,] allCase_Front = new Case_Script[5, 3];
    Case_Script[,] allCase_Back = new Case_Script[5, 3];

    public delegate void PiocheEvent(int nombre);
    public static PiocheEvent PiocheEffect;

    public delegate void DefausseEvent(int nombre);
    public static DefausseEvent DefausseEffect;

    private void Start()
    {
        Case_Script.triggerEffect += EventTriggered;

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

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;

                    SpawnParticle(particleEffect[4], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    FireEffect((int)effect.pos.y);

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;

                    SpawnParticle(particleEffect[0], particleEffectPos_Front[(int)effect.pos.y]);
                    SpawnParticle(particleEffect[0], particleEffectPos_Back[(int)effect.pos.y]);
                }
                effect.case_Script.EndMyTurn();
                break;

            case Card.EffectType.Water:
                if (effect.isColonne)
                {
                    WaterEffectColonne((int)effect.pos.x);

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;

                    SpawnParticle(particleEffect[5], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    WaterEffect((int)effect.pos.y);

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;

                    SpawnParticle(particleEffect[1], particleEffectPos_Front[(int)effect.pos.y]);
                    SpawnParticle(particleEffect[1], particleEffectPos_Back[(int)effect.pos.y]);
                }
                effect.case_Script.EndMyTurn();
                break;

            case Card.EffectType.Oil:
                if (effect.isColonne)
                {
                    OilEffectColonne((int)effect.pos.x);

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;

                    SpawnParticle(particleEffect[6], particleEffectPos_Colonne[(int)effect.pos.x]);
                }
                else
                {
                    OilEffect((int)effect.pos.y);

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;

                    SpawnParticle(particleEffect[2], particleEffectPos_Front[(int)effect.pos.y]);
                    SpawnParticle(particleEffect[2], particleEffectPos_Back[(int)effect.pos.y]);
                }
                effect.case_Script.EndMyTurn();
                break;

            case Card.EffectType.Encouragement:
                EncouragementEffect((int)effect.pos.y);

                effect.case_Script.SetCard(null);
                effect.case_Script.isEmpty = true;

                SpawnParticle(particleEffect[3], particleEffectPos_Front[(int)effect.pos.y]);

                effect.case_Script.EndMyTurn();

                break;

            case Card.EffectType.Defausse:
                DefausseEffect(effect.numCardIfNeeded);
                break;

            case Card.EffectType.Pioche:
                PiocheEffect(effect.numCardIfNeeded);
                break;

            case Card.EffectType.Rafle:
                if (effect.isColonne)
                {
                    PiocheEffect(GetNonEmptyCaseColonne((int)effect.pos.x));

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;
                }
                else
                {
                    PiocheEffect(GetNonEmptyCase((int)effect.pos.y));

                    effect.case_Script.SetCard(null);
                    effect.case_Script.isEmpty = true;
                }
                break;

            case Card.EffectType.Zap:
                ZapEffect(effect.case_Script);
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
        case_Script.SetCard(null);
        case_Script.isEmpty = true;

        PiocheEffect(1);
    }

    void EncouragementEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isEncouraged = true;
        }
    }

    void WaterEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isWatered = true;
            allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isWatered = true;
        }
    }

    void WaterEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>().isWatered = true;
            allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>().isWatered = true;
        }
    }

    void FireEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isFired = true;
            allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isFired = true;
        }
    }

    void FireEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>().isFired = true;
            allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>().isFired = true;
        }
    }

    void OilEffect(int posY)
    {
        for (int i = 0; i < 5; i++)
        {
            allCase_Front[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isOiled = true;
            allCase_Back[i, posY].gameObject.GetComponent<Case_Effect_Manager>().isOiled = true;
        }
    }

    void OilEffectColonne(int posX)
    {
        for (int i = 0; i < 3; i++)
        {
            allCase_Front[posX, i].gameObject.GetComponent<Case_Effect_Manager>().isOiled = true;
            allCase_Back[posX, i].gameObject.GetComponent<Case_Effect_Manager>().isOiled = true;
        }
    }
}
