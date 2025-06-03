using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccessoryStat 
{
    public StatType StatType;
    public float Amount;

    public AccessoryStat(StatType type, float amount)
    {
        StatType = type;
        Amount = amount;
    }
}
public enum StatType
{
    Unknown,
    Damage,
    Defence,
    CritStrike,
    Reflection,
    StunChance,
    MaxArmour,
    MaxHP,
    RegenHP
}
