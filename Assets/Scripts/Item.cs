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

    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }

    IEnumerator handleItemDisappear()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    IEnumerator addToInventory()
    {
        yield return new WaitForSeconds(0.1f);
        InventoryManager.instance.Add(this);
    }

    public IEnumerator MoveTowardsPlayer(GameObject player)
    {
        while (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            yield return null;
        }
       
    }
    
    public void PickUp()
    {
        StartCoroutine(MoveTowardsPlayer(GameObject.FindGameObjectWithTag("Player")));
        StartCoroutine(addToInventory());
        StartCoroutine(handleItemDisappear());
    }

    public void use()
    {
       
    }

}