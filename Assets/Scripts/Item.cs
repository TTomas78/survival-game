using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int resourceGater = 1;
    [SerializeField] public bool pickable;
    public string name_id;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pickable)
        {
            // add item to inventory
            InventoryManager.instance.Add(this);
            StartCoroutine(handleItemDisappear());
            // Debug.Log("Item picked up");
        }
    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }


    IEnumerator handleItemDisappear()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }




}