using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListUI : MonoBehaviour
{
    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI

    ItemListManager itemList;    // Our current inventory

    ItemListSlot[] slots;	// List of all the slots

    
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ItemListSlot>();
        itemList = ItemListManager.instance;
        itemList.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void UpdateUI()
    {
        Debug.Log("Updating Inventory UI");
        foreach (Item item in itemList.items)
        {
           // iterate slots and add items
            foreach (ItemListSlot slot in slots)
            {
                if (slot.item == null)
                {
                    slot.AddItem(item);
                    break;
                }
            }
            
        }
    }
}
