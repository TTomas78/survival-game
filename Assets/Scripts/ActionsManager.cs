using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{

    InventoryManager inventory;
    public static ActionsManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;

    }

    void Start()
    {
        inventory = InventoryManager.instance;
    }

    public void DispatchAction(Resource resource)
    {
        if (SearchItem(resource.RequiredAction).Count == 0)
        {
            Debug.Log("not possible to perform the accion due to an ausence of items");
        }

        //if the player get's the item -> -1 is now hardcoded but could depend on the weapon stat
        resource.SetHealth(resource.Health - 1);

        //Space to decrease the item duration if we'd like that


        //start item corrutine
        StartCoroutine(SpawnRewardItems(resource.RewardPrefab, resource.transform.position, resource.SpawnRewardRadius));

    }
    //search an item to perform an specific actionm now it returns a boolean but probably should return the item in the future
    private List<Item> SearchItem(Enums.PossibleActions requiredAction)
    {
        //from now on it will return the first item, but while we implemented new stats on the items, we should select the most optimal
        List<Item> possibleItems = inventory.items.Where(item => item.actions.Contains(requiredAction)).ToList();

        return possibleItems;
    }

    IEnumerator SpawnRewardItems(GameObject rewardPrefab,Vector3 position,float spawnRadius)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject item = Instantiate(rewardPrefab, position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        item.GetComponent<Item>().name_id = rewardPrefab.name;
    }
}
