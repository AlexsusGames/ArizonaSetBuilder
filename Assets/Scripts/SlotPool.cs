using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPool : MonoBehaviour
{
    [SerializeField] private RectTransform slotPrefab;

    private List<SlotView> slots;

    public void ShowSlots(List<ItemConfig> itemConfig)
    {
        HideAll();

        if (slots == null)
            CreatePool(itemConfig.Count);

        for (int i = 0; i < itemConfig.Count; i++)
        {
            var slot = GetFreeSlot();

            slot.gameObject.SetActive(true);

            slot.SetData(itemConfig[i]);
        }
    }

    private void HideAll()
    {
        if(slots != null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    private SlotView GetFreeSlot()
    {
        for (int i = 0;i < slots.Count; i++)
        {
            if (!slots[i].gameObject.activeInHierarchy)
                return slots[i];
        }

        return CreateSlot();
    }

    private void CreatePool(int size)
    {
        slots = new();

        for (int i = 0; i < size; i++)
        {
            CreateSlot();
        }
    }

    private SlotView CreateSlot()
    {
        var slot = Instantiate(slotPrefab, transform);

        slot.TryGetComponent(out SlotView slotView);
        slots.Add(slotView);

        slot.gameObject.SetActive(false);

        return slotView;
    }
}
