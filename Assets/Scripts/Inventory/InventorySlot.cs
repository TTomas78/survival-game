using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text countText;
    public StackItem stackItem;  // Current item in the slot

    public void AddItem(StackItem newItem)
    {
        stackItem = newItem; // Set our item

        stackItem.item.positionInInventory = transform.GetSiblingIndex(); // set the position in the inventory
 
        // Debug.Log("Added item: " + item);
        SpriteRenderer sr = stackItem.item.gameObject.GetComponent<SpriteRenderer>();
        icon.sprite = sr.sprite; // Change the icon
        icon.enabled = true; // Enable the icon
        removeButton.interactable = true; // Enable the button
        countText.text = stackItem.quantity.ToString(); // Set the count
        countText.enabled = true; // Enable the text
    }

    public void ClearSlot()
    {
        stackItem = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        countText.enabled = false;
    }


    // Called when the remove button is pressed
    public void OnRemoveButton()
    {
        InventoryManager.instance.Remove(stackItem);
    }

    // swap slots content
    public void SwapSlot(InventorySlot targetSlot)
    {
        StackItem temp = stackItem;

        if (targetSlot.stackItem == null)
            ClearSlot();
        else
            AddItem(targetSlot.stackItem);

        targetSlot.AddItem(temp);
    }

    public void UpdateSlot()
    {
        countText.text = stackItem.quantity.ToString();
    }

}
