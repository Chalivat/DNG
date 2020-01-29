﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public new string name;
    public int type;
    public string description;
    public int damage;
    
    public enum EffectType {None,Fire,Water,Oil,Zap,Rafle,Pioche,Defausse,Encouragement};
    public EffectType effectType;

    public int nombrePioche;

    //public Effect effect;

    [Header("Visual")]

    public Texture artwork;

    public Color borderColor;
    public Color fireColor;
}
