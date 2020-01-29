using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Script : MonoBehaviour
{
    public Vector2 pos;
    public int ligne;
    public bool isColonne = false;

    public Card card;

    public int power = 0;

    public delegate void EventEffect(EffectClass effect);
    public static EventEffect triggerEffect;

    public delegate void EndTurn();
    public static EndTurn EndTheTurn;

    public bool isEmpty = true;

    public bool Check(int type)
    {
        if(ligne == type || type == 3)
        {
            Case_Effect_Manager effectManager = GetComponent<Case_Effect_Manager>();
            if (effectManager)
            {
                if (!effectManager.isOiled)
                    return true;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public void PlacerCarte(Card newCard)
    {
        isEmpty = false;
        SetCard(newCard);

        if (card.effectType != Card.EffectType.None)
        {
            EffectClass effect = new EffectClass(card.effectType, card.nombrePioche,pos,ligne,this,isColonne);
            triggerEffect(effect);
        }
        else
        {
            EndMyTurn();
        }

        //UpdateBoard();
    }

    void UpdateBoard()
    {
        GameObject.Find("Board").transform.GetChild(0).GetComponent<Board_Script>().UpdateBoardCases();
    }

    public void SetCard(Card card)
    {
        this.card = card;
        if (card != null)
            power = card.damage;
        else
            power = 0;
    }

    public void EndMyTurn()
    {
        EndTheTurn();
    }
}

public class EffectClass
{
    public Card.EffectType effectType;
    public int numCardIfNeeded;

    public Vector2 pos;
    public int ligne;

    public bool isColonne;

    public Case_Script case_Script;

    public EffectClass(Card.EffectType type, int num, Vector2 pos, int ligne, Case_Script case_Script, bool isColonne)
    {
        effectType = type;
        numCardIfNeeded = num;
        this.ligne = ligne;
        this.pos = pos;
        this.case_Script = case_Script;
        this.isColonne = isColonne;
    }
}