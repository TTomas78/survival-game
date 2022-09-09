using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public EquipmentSlot slot;
    public int resourceGater;
    
    [SerializeField] public bool pickabe;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pickabe)
        {
            // add item to inventory
            InventoryManager.instance.Add(this);
            
            // pooling item
            gameObject.SetActive(false);
            // Debug.Log("Item picked up");
        }
    }






}

public enum EquipmentSlot {Weapon, Armor}