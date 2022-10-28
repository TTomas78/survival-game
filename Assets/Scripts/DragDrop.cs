using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : 
    MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IEndDragHandler, IDragHandler, IInitializePotentialDragHandler,
    IDropHandler

{
    [SerializeField] private Canvas canvas;
    private CanvasGroup canvasGroup;
    public InventorySlot slot;
    private RectTransform rectTransformImage;

    private void Awake()
    {

        // Get the rect transform for the slot icon
        rectTransformImage = slot.icon.GetComponent<RectTransform>();  
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("Begin Drag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        

    }
    
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("On Drag");
        rectTransformImage.anchoredPosition += eventData.delta / canvas.scaleFactor;
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject pointedObject = Physics2D.OverlapPoint(point).gameObject;

        //get the item based on the item in inventory
        InventorySlot initialSlot = eventData.pointerDrag.GetComponent<InventorySlot>();
        Item item = initialSlot.stackItem.item;

        //We should check if the selecte object implements the interface
        if (pointedObject.GetComponent<IDrageable>().OnDropObject(item))
        {
            InventoryManager.instance.SubstractItem(initialSlot.stackItem, 1);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransformImage.anchoredPosition = Vector2.zero;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("On Pointer Down");
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot initialSlot = eventData.pointerDrag.GetComponent<InventorySlot>();
        InventorySlot targetSlot = GetComponent<InventorySlot>();

        InventoryManager.instance.SwapItems(initialSlot, targetSlot);
    }

}
