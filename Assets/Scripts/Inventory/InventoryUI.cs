using System.Globalization;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI

    InventoryManager inventory;    // Our current inventory

    InventorySlot[] slots;	// List of all the slots
    
    void Start()
    {
        inventory = InventoryManager.instance;

        inventory.onItemChangedCallback += UpdateUI;


        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI()
    {
        foreach (StackItem stackItem in inventory.items)
        {
            // search for the item in the inventory & update its quantity
            bool found = false;
            foreach (InventorySlot slot in slots)
            {
                if(slot.stackItem?.item?.name_id == stackItem.item.name_id)
                {
                    slot.stackItem.quantity = stackItem.quantity;
                    slot.UpdateSlot();
                    found = true;
                    break;
                }
            }
            // if not found add it to the inventory
            if (!found)
            {
                foreach (InventorySlot slot in slots)
                {
                    if (slot.stackItem?.item == null)
                    {
                        slot.AddItem(stackItem);
                        break;
                    }
                }
            }
            
        }
        // find empty slots and clear them
        foreach (InventorySlot slot in slots)
        {
            if(slot.stackItem?.item != null)
            {
                bool found = false;
                foreach (StackItem stackItem in inventory.items)
                {
                    if (stackItem.item.name_id == slot.stackItem.item.name_id)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    slot.ClearSlot();
            }
        }
    }

}
