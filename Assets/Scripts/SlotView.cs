using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotView : MonoBehaviour
{
    [SerializeField] private Image slotImage;

    protected ItemConfig config;
    public ItemConfig Config => config;

    public virtual void SetData(ItemConfig config)
    {
        slotImage.gameObject.SetActive(true);

        this.config = config;

        slotImage.sprite = config.Icon;
    }

    public virtual void HideItem() => slotImage.gameObject.SetActive(false);
}
