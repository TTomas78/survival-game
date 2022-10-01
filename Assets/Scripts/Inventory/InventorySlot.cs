using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text countText;
    public Item item;  // Current item in the slot

    public void AddItem(Item newItem)
    {
        item = newItem; // Set our item


        item.positionInInventory = transform.GetSiblingIndex(); ; // set the position in the inventory
 
        // Debug.Log("Added item: " + item);
        SpriteRenderer sr = item.gameObject.GetComponent<SpriteRenderer>();
        icon.sprite = sr.sprite; // Change the icon
        icon.enabled = true; // Enable the icon
        removeButton.interactable = true; // Enable the button
        countText.text = item.resourceGater.ToString(); // Set the count
        countText.enabled = true; // Enable the text
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        countText.enabled = false;
    }


    // Called when the remove button is pressed
    public void OnRemoveButton()
    {
        InventoryManager.instance.Remove(item);
    }

    // swap slots content
    public void SwapSlot(InventorySlot targetSlot)
    {
        Item temp = item;

        if (targetSlot.item == null)
            ClearSlot();
        else
            AddItem(targetSlot.item);

        targetSlot.AddItem(temp);
    }

}
