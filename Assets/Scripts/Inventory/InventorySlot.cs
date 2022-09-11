using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TMP_Text countText;

    public Item item;
    public void AddItem(Item newItem)
    {
        item = newItem;
        Debug.Log("Added item: " + item);
        SpriteRenderer sr = item.gameObject.GetComponent<SpriteRenderer>();
        icon.sprite = sr.sprite;
        icon.enabled = true;
        removeButton.interactable = true;
        countText.text = item.resourceGater.ToString();
        countText.enabled = true;
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
    public void SwapSlot(InventorySlot slot)
    {
        Item temp = item;
        if(slot.item == null )
        {
            ClearSlot();
        } else
        {
            AddItem(slot.item);   
        }
        slot.AddItem(temp);
    }

}
