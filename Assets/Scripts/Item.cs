using UnityEngine;

public class Item : MonoBehaviour
{
    public int resourceGater = 1;
    [SerializeField] public bool pickable;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pickable)
        {
            // add item to inventory
            InventoryManager.instance.Add(this);

            // pooling item
            gameObject.SetActive(false);
            // Debug.Log("Item picked up");
        }
    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }

}