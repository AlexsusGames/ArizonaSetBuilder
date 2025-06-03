using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Accessory", fileName = "AccessoryConfig")]
public class AccessoryConfig : ItemConfig
{
    public SlotType SlotType;

    public AccessoryStat[] DefaultStats;
    public AccessoryStat[] YellowStats;

    public StatType[] ImprovingStats;

    public bool CanBeImprovedTo13;
}

public enum SlotType
{
    Unknown,
    First,
    Second,
    Third,
    Fourth,
    Fifth,
    Sixth,
    Armour,
    Skin,
    Bag
}
