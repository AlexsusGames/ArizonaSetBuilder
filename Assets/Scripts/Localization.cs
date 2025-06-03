using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization 
{
    private string[] statLocalization = { "Все", "Урон", "Защита", "Удача", "Отражение", "Стан.Шанс", "Броня", "ХП", "Реген" };
    private string[] slotLocalization = { "Все", "1 слот", "2 слот", "3 слот", "4 слот", "5 слот", "6 слот", "Броня", "Скин", "Сумка"};

    public string LocalizeStat(StatType statType) => statLocalization[(int)statType];
    public string LocalizeSlot(SlotType slotType) => slotLocalization[(int)slotType];

    public string ConverStatToString(AccessoryStat stat)
    {
        switch (stat.StatType)
        {
            case StatType.Damage: return $"+{stat.Amount} к урону";
            case StatType.Defence: return $"-{stat.Amount}% от урона";
            case StatType.CritStrike: return $"+{stat.Amount}% к шансу крита";
            case StatType.Reflection: return $"+{stat.Amount}% к отражению";
            case StatType.StunChance: return $"+{stat.Amount}% к шансу оглушить";
            case StatType.MaxArmour: return $"+{stat.Amount} к макс.Броне";
            case StatType.MaxHP: return $"+{stat.Amount} к макс.Здоровью";
            case StatType.RegenHP: return $"+{stat.Amount} ХП в минуту";
            default: return "";
        }
    }

    public string ConvertStatToLongString(AccessoryStat stat) => $"{LocalizeStat(stat.StatType)}: {ConverStatToString(stat)}";
}
