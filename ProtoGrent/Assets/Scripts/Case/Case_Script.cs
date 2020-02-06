using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case_Script : MonoBehaviour
{
    public Vector2 pos;
    public int ligne;
    public bool isColonne = false;
    public uint boardNumber;

    public Card card;

    public int power = 0;

    public Case_Effect_Manager effectManager;

    public delegate void EventEffect(EffectClass effect);
    public static EventEffect triggerEffect;

    public delegate void EndTurn();
    public static EndTurn EndTheTurn;

    public delegate void CardPlaced();
    public static CardPlaced cardPlaced;

    public delegate void Action(PlayerCoup coup);
    public static Action playerAction;

    public bool isEffect = false;
    public bool isEmpty = true;

    public bool Check(int type, uint board)
    {
        if(ligne == type && boardNumber == board || type == 3 && boardNumber == board || isColonne && isEffect)
        {
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
        playerAction(new PlayerCoup(this, card, LigneHighlight_Script.activeBoard));

        if (card.type <= 2)
        {
            SpawnUnitOnBoard();
        }

        if (card.effectType != Card.EffectType.None)
        {
            EffectClass effect = new EffectClass(card.effectType, card.nombrePioche,pos,ligne,this,isColonne);
            triggerEffect(effect);
        }
        else
        {
            EndMyTurn();
        }
        CountPointOnCase();
        GameObject.Find("Front_Board").GetComponent<Board_Script>().CountPoint();
        GameObject.Find("Back_Board").GetComponent<Board_Script>().CountPoint();
        cardPlaced();
    }

    public void SpawnUnitOnBoard()
    {
        Case_Unit_Manager case_Unit_Manager = GetComponent<Case_Unit_Manager>();
        if(case_Unit_Manager != null)
        case_Unit_Manager.SetUnitOnCase(GameObject.Find("Board").GetComponent<Unit_Script>().WhichUnity(card.damage,card.type,transform));
    }

    public void SetCard(Card newCard)
    {
        this.card = newCard;
    }

    public void CountPointOnCase()
    {
        if (card != null)
        {
            power = card.damage;
            if (effectManager.isFired || effectManager.isWatered)
            {
                power = 1;
            }
            if (effectManager.isEncouraged)
            {
                power = card.damage * 2;
            }
        }
        else
        {
            power = 0;
        }
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