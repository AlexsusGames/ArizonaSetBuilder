using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetStatsResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;

    private void Clear()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = string.Empty;
        }
    }

    public void UpdateView(Accessory[] accessories)
    {
        Dictionary<StatType, float> allStats = new Dictionary<StatType, float>();

        Clear();

        for (int i = 0; i < accessories.Length; i++)
        {
            if (accessories[i] == null) continue;

            var stats = accessories[i].GetAllStats();

            for (int j = 0; j < stats.Count; j++)
            {
                var type = stats[j].StatType;
                var amount = stats[j].Amount;

                allStats[type] = allStats.GetValueOrDefault(type) + amount;
            }
        }

        int index = 0;

        foreach (var type in allStats.Keys)
        {
            texts[index].text = $"{Core.Localization.LocalizeStat(type)} - <color=yellow>{allStats[type]}";

            index++;
        }
    }
}
