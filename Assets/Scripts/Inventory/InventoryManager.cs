using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items = new List<Item>();
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

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        // stack items 
        foreach (Item i in items)
        {
            if (i.name_id == item.name_id)
            {
                i.resourceGater += item.resourceGater;
                // Debug.Log(item.name + " resourceGater +1");
                Destroy(item.gameObject);
                // Trigger callback
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return true;
            }
        }

        items.Add(item);

        // Trigger callback
        if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

        // Debug.Log(item.name + " was added.");
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
