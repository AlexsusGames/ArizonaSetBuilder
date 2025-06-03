using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SetBuilder : MonoBehaviour
{
    [SerializeField] private AccessoryPreviewSlot[] slots;

    [SerializeField] private AccessoryView accessoryView;
    [SerializeField] private SetStatsResultView statsView;
 
    private Dictionary<PreviewSlot, Accessory> accessoryMap;

    private void Start()
    {
        CreatSlotMap();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ConfigChanged += OnSetChanged;
            slots[i].AssignListener(this);
        }
    }

    public void UpdateSetStatsView() => statsView.UpdateView(accessoryMap.Values.ToArray());

    public void ShowView(PreviewSlot slot)
    {
        var data = accessoryMap[slot];

        accessoryView.Show(data);
    }

    public void TakeOffAccessory(Accessory accessory)
    {
        PreviewSlot key = null;

        foreach (var item in accessoryMap.Keys)
        {
            if (accessoryMap[item] == accessory)
            {
                item.HideItem();

                key = item;
            }
        }

        if(key != null)
        {
            accessoryMap[key] = null;
            UpdateSetStatsView();
        }
    }

    private void CreatSlotMap()
    {
        accessoryMap = new Dictionary<PreviewSlot, Accessory>();

        foreach (var slot in slots)
        {
            accessoryMap[slot] = null;
        }
    }

    public void OnSetChanged(PreviewSlot slot, ItemConfig config)
    {
        if(config is AccessoryConfig accessoryConfig)
        {
            accessoryMap[slot] = new Accessory(accessoryConfig);
            UpdateSetStatsView();
        }
    }

}
