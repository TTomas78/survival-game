using System.Globalization;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI
    public CraftingUI craftingUI;

    InventoryManager inventory;    // Our current inventory

    InventorySlot[] slots;	// List of all the slots
    
    void Start()
    {
        inventory = InventoryManager.instance;

        craftingUI = FindObjectOfType<CraftingUI>();

        inventory.onItemChangedCallback += UpdateUI;


        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    void UpdateUI()
    {
         Debug.Log("Updating Inventory UI");
        Debug.Log(inventory.items.Count);
        foreach (Item item in inventory.items)
        {
            // search for the item in the inventory
            bool found = false;
            foreach (InventorySlot slot in slots)
            {
                if(slot.item != null && slot.item.name_id == item.name_id)
                {
                    // Debug.Log("slot.item.name_id: " + slot.item.name_id);
                    // Debug.Log("item.name_id: " + item.name_id);
                    slots[item.positionInInventory].AddItem(item);
                    found = true;
                    // Debug.Log("Found");

                }
            }
            // if the item is not found in the inventory, add it
            if (!found)
            {
                foreach (InventorySlot slot in slots)
                {
                    if (slot.item == null)
                    {
                        slot.AddItem(item);
                        // Debug.Log("not Found but added");
                        break;
                    }
                }
            }
            
        }
        // find slot.item in inventory.items
        // if not found, clear slot
        foreach (InventorySlot slot in slots)
        {
            if(slot.item != null)
            {
                bool found = false;
                foreach (Item item in inventory.items)
                {
                    if (item.name_id == slot.item.name_id)
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
