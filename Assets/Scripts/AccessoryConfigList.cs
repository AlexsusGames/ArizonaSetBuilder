using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccessoryConfigList : MonoBehaviour
{
    [SerializeField] private SearchFilter filter;
    [SerializeField] private SlotPool pool;

    private ItemConfig[] allItems;

    private void Start()
    {
        allItems = Resources.LoadAll<ItemConfig>("");

        filter.OnSettingsChanged += FindByFilters;

        FindByFilters(new SearchSettings(string.Empty, SlotType.Unknown, StatType.Unknown));
    }

    public void FindByFilters(SearchSettings settings)
    {
        List<ItemConfig> items = new List<ItemConfig>();

        for (int i = 0; i < allItems.Length; i++)
        {
            if(CheckSettings(settings, allItems[i]))
            {
                items.Add(allItems[i]);
            }
        }

        pool.ShowSlots(items);
    }

    private bool CheckSettings(SearchSettings settings, ItemConfig config)
    {
        string inputName = string.IsNullOrEmpty(settings.Name) ? string.Empty : settings.Name;

        string configName = config.Name.ToLower();

        bool nameChecked = string.IsNullOrEmpty(inputName) || configName.Contains(inputName);

        bool statChecked = settings.StatType == StatType.Unknown || CheckAccessoryStat(config, settings.StatType);

        bool slotChecked = settings.SlotType == SlotType.Unknown || CheckAccessorySlot(config, settings.SlotType);

        return nameChecked && statChecked && slotChecked;
    }

    private bool CheckAccessorySlot(ItemConfig config, SlotType type)
    {
        if (config is AccessoryConfig accessoryConfig)
        {
            return accessoryConfig.SlotType == type;
        }
        else return false;
    }

    private bool CheckAccessoryStat(ItemConfig config, StatType type)
    {
        if(config is AccessoryConfig accessoryConfig)
        {
            var defaultStats = accessoryConfig.DefaultStats;

            for (int i = 0; i < defaultStats.Length; i++)
            {
                if (defaultStats[i].StatType == type)
                    return true;
            }

            var yellowStats = accessoryConfig.YellowStats;

            for (int i = 0; i < yellowStats.Length; i++)
            {
                if (yellowStats[i].StatType == type)
                    return true;
            }

            return false;
        }
        else return false;
    }
}
