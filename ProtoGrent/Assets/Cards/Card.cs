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

    public Color contourColor;
    
    public enum EffectType {None,Fire,Water,Oil,Zap,Rafle,Pioche,Defausse,Encouragement,Clean};
    public EffectType effectType;

    public int nombrePioche;

    //SI ESPION SE PLACE DANS LE CAMP ADVERSE
    public bool isEspion;

    [Header("Visual")]

    public Sprite artwork;

    public bool asDescription;
}
