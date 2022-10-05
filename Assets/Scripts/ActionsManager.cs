using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{

    InventoryManager inventory;
    public static ActionsManager instance;
    Player player;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Action manager found!");
            return;
        }
        instance = this;

    }

    void Start()
    {
        inventory = InventoryManager.instance;
        player = FindObjectOfType<Player>();
    }

    public void DispatchAction(Resource resource)
    {
        Tool selectedTool = SearchTool(resource.RequiredAction);
        if (selectedTool == null)
        {
            return;
        }

        resource.SetHealth(resource.Health - selectedTool.Damage);


        selectedTool.decreaseDurability(1);

        //start item corrutine
        StartCoroutine(SpawnRewardItems(resource.RewardPrefab, resource.transform.position, resource.SpawnRewardRadius));

    }
    //search an item to perform an specific actionm now it returns a boolean but probably should return the item in the future
    private Tool SearchTool(Enums.PossibleActions requiredAction)
    {
        //from now on it will return the first item, but while we implemented new stats on the items, we should select the most optimal
        List<Tool> possibleItems = new List<Tool>();

        foreach(Item item in inventory.items)
        {
            if (item is Tool)
            {
                possibleItems.Add((Tool)item);
            }
        };
        possibleItems = possibleItems.Where(item => item.actions.Contains(requiredAction)).ToList();

        Tool selectedTool = null;

        foreach (Tool tool in possibleItems)
        {
            if ((selectedTool == null && tool.Durability > 0) || (selectedTool != null &&  tool.positionInInventory < selectedTool.positionInInventory && tool.Durability > 0))
            {
                selectedTool = tool;
            }
        };
        
        return selectedTool;
    }

    IEnumerator SpawnRewardItems(GameObject rewardPrefab,Vector3 position,float spawnRadius)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject item = Instantiate(rewardPrefab, position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        item.GetComponent<Item>().name_id = rewardPrefab.name;
    }

    public void UseConsumible(Consumible consumible)
    {
        //TODO implement this
    }
}
