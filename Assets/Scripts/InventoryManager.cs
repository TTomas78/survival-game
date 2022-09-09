using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> inventory = new List<Item>();
    public int space = 20;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    public bool Add(Item item)
    {
        if (inventory.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        // stack items 
        foreach (Item i in inventory)
        {
            if (i.name == item.name)
            {
                i.resourceGater += item.resourceGater;
                Destroy(item.gameObject);
                // Debug.Log(item.name + " resourceGater +1");
                
                return true;
            }
        }
        inventory.Add(item);
        // Debug.Log(item.name + " was added.");
        return true;
    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
    }
}
