using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int resourceGater = 1;
    protected ActionsManager actionsManager;
    [SerializeField] public bool pickable;
    public string name_id;
    public int positionInInventory = -1;
    //TODO delete this
    public List<Enums.PossibleActions> actions;

    void Awake()
    {
        actionsManager = FindObjectOfType<ActionsManager>();

    }

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

    public void use()
    {
       
    }

}