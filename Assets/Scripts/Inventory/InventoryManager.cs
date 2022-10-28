using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<StackItem> items = new List<StackItem>();
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
        foreach (StackItem stackItem in items)
        {
            if (stackItem.item.name_id == item.name_id)
            {
                stackItem.quantity++;
                Destroy(item);
                onItemChangedCallback.Invoke();
                return true;
            }

        }

        // add new item
        StackItem newItem = new StackItem();
        newItem.item = item;
        newItem.quantity = 1;

        items.Add(newItem);

        // Trigger callback
        if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(StackItem stackItem)
    {
        items.Remove(stackItem);

        // get the player position
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        // for iterate with quantity 
        for (int i = 0; i < stackItem.quantity; i++)
        {
            // create a new item
            Vector3 offset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);

            Item newItem = Instantiate(stackItem.item, playerPos + offset, Quaternion.identity);
            newItem.name_id = stackItem.item.name_id;
            newItem.name = stackItem.item.name_id;
            newItem.positionInInventory = -1;

            newItem.gameObject.SetActive(true);

        }


        // Destroy(stackItem.item.gameObject);

        // Trigger callback to the UI
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SwapItems(InventorySlot initialSlot, InventorySlot targetSlot)
    {
        // swap items
        if (initialSlot.stackItem.item != null)
            initialSlot.SwapSlot(targetSlot);
    }

    // search item by name
    public StackItem SearchItemByName(string name)
    {
        foreach (StackItem i in items)
        {
            if (i.item.name_id == name)
            {
                return i;
            }
        }
        return null;
    }

    // search item id by name
    public int SearchItemIndexByName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.name_id == name)
            {
                return i;
            }
        }
        return -1;
    }

    // subtract item by name
    public void SubtractItemByName(string name, int amount)
    {
        foreach (StackItem i in items)
        {
            if (i.item.name_id == name)
            {
                i.quantity -= amount;
                if (i.quantity <= 0)
                {
                    Remove(i);
                }
                return;
            }
        }
    }

    public void SubstractItem(StackItem stackItem, int amount)
    {
        if (!(stackItem.quantity < amount))
        {
            stackItem.quantity -= amount;
            if (stackItem.quantity <= 0)
            {
                items.Remove(stackItem);
            }
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    public bool HasItem(string name)
    {
        foreach (StackItem i in items)
        {
            if (i.item.name_id == name)
            {
                return true;
            }
        }
        return false;
    }
}
