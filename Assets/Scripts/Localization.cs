using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization 
{
    private string[] statLocalization = { "���", "����", "������", "�����", "���������", "����.����", "�����", "��", "�����" };
    private string[] slotLocalization = { "���", "1 ����", "2 ����", "3 ����", "4 ����", "5 ����", "6 ����", "�����", "����", "�����"};

    public string LocalizeStat(StatType statType) => statLocalization[(int)statType];
    public string LocalizeSlot(SlotType slotType) => slotLocalization[(int)slotType];

    public string ConverStatToString(AccessoryStat stat)
    {
        switch (stat.StatType)
        {
            case StatType.Damage: return $"+{stat.Amount} � �����";
            case StatType.Defence: return $"-{stat.Amount}% �� �����";
            case StatType.CritStrike: return $"+{stat.Amount}% � ����� �����";
            case StatType.Reflection: return $"+{stat.Amount}% � ���������";
            case StatType.StunChance: return $"+{stat.Amount}% � ����� ��������";
            case StatType.MaxArmour: return $"+{stat.Amount} � ����.�����";
            case StatType.MaxHP: return $"+{stat.Amount} � ����.��������";
            case StatType.RegenHP: return $"+{stat.Amount} �� � ������";
            default: return "";
        }
    }

    public string ConvertStatToLongString(AccessoryStat stat) => $"{LocalizeStat(stat.StatType)}: {ConverStatToString(stat)}";
}
