using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchFilter : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown slotDropdown;
    [SerializeField] private TMP_Dropdown statDropdown;

    [SerializeField] private TMP_InputField inputField;

    private SearchSettings setting;

    public event Action<SearchSettings> OnSettingsChanged;

    private void Start()
    {
        Fill();

        inputField.onValueChanged.AddListener(SearchedNameChanged);

        slotDropdown.onValueChanged.AddListener(OnSlotSettingsChanged);
        statDropdown.onValueChanged.AddListener(OnStatSettingsChanged);
    }

    private void Fill()
    {
        var statOption = CreateOption(typeof(StatType));
        statDropdown.options = statOption.options;

        var slotOption = CreateOption(typeof(SlotType));
        slotDropdown.options = slotOption.options;
    }

    private TMP_Dropdown.OptionDataList CreateOption(Type enumType)
    {
        var enumValues = Enum.GetValues(enumType);
        TMP_Dropdown.OptionDataList list = new TMP_Dropdown.OptionDataList();

        for (int i = 0; i < enumValues.Length; i++)
        {
            string optionText = enumType == typeof(StatType) ? Core.Localization.LocalizeStat((StatType)i) : Core.Localization.LocalizeSlot((SlotType)i);
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(optionText);
            list.options.Add(option);
        }

        return list;
    }

    private void SearchedNameChanged(string text)
    {
        setting.Name = text;
        OnSettingsChanged?.Invoke(setting);
    }

    private void OnStatSettingsChanged(int stat)
    {
        StatType type = (StatType)stat;

        setting.StatType = type;

        OnSettingsChanged?.Invoke(setting);
    }

    private void OnSlotSettingsChanged(int slot)
    {
        SlotType type = (SlotType)slot;

        setting.SlotType = type;

        OnSettingsChanged?.Invoke(setting);
    }

    public void ResetStat()
    {
        statDropdown.value = 0;

        OnStatSettingsChanged(0);
    }
    public void ResetSlot()
    {
        slotDropdown.value = 0;

        OnSlotSettingsChanged(0);
    }
}
