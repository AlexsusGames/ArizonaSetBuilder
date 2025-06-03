using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreviewSlot : SlotView, IDropHandler
{
    [SerializeField] private GameObject previewImage;

    public event Action<PreviewSlot, ItemConfig> ConfigChanged;

    protected Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public override void SetData(ItemConfig config)
    {
        base.SetData(config);

        ChangePreviewImageEnabled(false);

        ConfigChanged?.Invoke(this, config);
    }

    public override void HideItem()
    {
        base.HideItem();

        ChangePreviewImageEnabled(true);
    }

    public void AssignListener(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    private void ChangePreviewImageEnabled(bool enabled)
    {
        if (previewImage != null)
            previewImage.gameObject.SetActive(enabled);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.gameObject.TryGetComponent(out DragableSlot dragableSlot))
        {
            SetData(dragableSlot.Config);
        }
    }
}
