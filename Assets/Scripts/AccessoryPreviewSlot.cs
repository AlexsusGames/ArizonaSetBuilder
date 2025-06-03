using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AccessoryPreviewSlot : PreviewSlot
{
    [SerializeField] private SlotType slotType;

    public override void SetData(ItemConfig config)
    {
        if (config is AccessoryConfig accessoryConfig && accessoryConfig.SlotType == slotType)
        {
            base.SetData(config);
        }
    }

    public void AssignListener(SetBuilder setBuiler)
    {
        UnityAction action = () => setBuiler.ShowView(this);

        AssignListener(action);
    }

    public void ChangeSlotType(SlotType type) => slotType = type;
}
