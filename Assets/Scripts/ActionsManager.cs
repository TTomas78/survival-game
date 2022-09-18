using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class ActionsManager : MonoBehaviour
    //must be a singleton
{
    [SerializeField] InventoryManager inventory;
    public void DispatchAction(Resource resource)
    {
        //changed required action for the resource required action
        if (!SearchItem(resource.RequiredAction))
        {
            Debug.Log("not possible to perform the accion due to an ausence of items");
        }
        //now we should check if there are any usable item if(usableItem != null)

        //if the player get's the item -> -1 is now hardcoded but could depend on the weapon stat
        resource.SetHealth(resource.Health - 1);

        //Space to decrease the item duration if we'd like that


        //start item corrutine
        StartCoroutine(SpawnRewardItems(resource.RewardPrefab, resource.transform.position, resource.SpawnRewardRadius));

    }
    //search an item to perform an specific actionm now it returns a boolean but probably should return the item in the future
    private bool SearchItem(Enums.PossibleActions requiredAction)
    {
        //from now on it will return the first item, but while we implemented new stats on the items, we should select the most optimal
        List<Item> possibleItems = inventory.items.Where(item => item.actions.Contains(requiredAction)).ToList();

        if(possibleItems.Count != 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator SpawnRewardItems(GameObject rewardPrefab,Vector3 position,float spawnRadius)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject item = Instantiate(rewardPrefab, position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        item.GetComponent<Item>().name_id = rewardPrefab.name;
    }
}