using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        // get the player position
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        // for iterate with resourcegater
        for (int i = 0; i < item.resourceGater; i++)
        {
            // position offset 
            Vector3 offset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
            // clone without memory refence & spawn the item
            Item clone = Instantiate(item, playerPos + offset, Quaternion.identity);
            // reset item props
            clone.resourceGater = 1;
            clone.gameObject.SetActive(true);
            clone.name = item.name_id;
        }

        // destroy gameObject reference
        Destroy(item.gameObject);

        // Trigger callback to the UI
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SwapItems(InventorySlot initialSlot, InventorySlot targetSlot)
    {
        // swap items
        if (initialSlot.item != null)
            initialSlot.SwapSlot(targetSlot);
    }

}
