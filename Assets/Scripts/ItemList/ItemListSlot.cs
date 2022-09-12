using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListSlot : MonoBehaviour
{
    public Item item;	// Current item in the slot
    public Image icon;

    public void AddItem(Item newItem)
    {
        item = newItem;

        SpriteRenderer sr = item.gameObject.GetComponent<SpriteRenderer>();
        icon.sprite = sr.sprite;
        icon.enabled = true; // Enable the icon
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    // spawn item onClick
    public void OnClick()
    {
        if (item != null)
        {
            ItemListManager.instance.SpawnItem(item);
        }
    }


}
