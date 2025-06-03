using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableSlot : SlotView, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private CanvasGroup canvasGroup;

    private Transform parent;
    private RectTransform rectTransform;

    public override void SetData(ItemConfig config)
    {
        rectTransform = GetComponent<RectTransform>();

        base.SetData(config);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;

        transform.parent = transform.root;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent);

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
}
