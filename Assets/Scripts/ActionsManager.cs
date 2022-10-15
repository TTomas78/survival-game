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
    public Enums.PossibleActions CurrentAction { get; set; }

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
        if (CurrentAction == resource.RequiredAction)
        {
            Tool selectedTool = SearchTool(resource.RequiredAction);
            if (selectedTool == null && resource.RequiredAction != Enums.PossibleActions.harvest)
            {
                Debug.Log("No tool found");
                return;
            }
            if (resource.RequiredAction == Enums.PossibleActions.harvest)
            {
                resource.Interact();
                return;
            }
            if (selectedTool != null)
            {
                if (selectedTool.Durability > 0)
                {
                    resource.Interact();
                    selectedTool.decreaseDurability(1);
                    // inventory.UpdateToolDurability(selectedTool);
                }
                else
                {
                    Debug.Log("Tool is broken");
                }
            }

            //start item corrutine
            // StartCoroutine(SpawnRewardItems(resource.RewardPrefab, resource.transform.position, resource.SpawnRewardRadius));
        }

    }
    //search an item to perform an specific actionm now it returns a boolean but probably should return the item in the future
    private Tool SearchTool(Enums.PossibleActions requiredAction)
    {
        //from now on it will return the first item, but while we implemented new stats on the items, we should select the most optimal
        List<Tool> possibleItems = new List<Tool>();

        foreach(StackItem stackItem in inventory.items)
        {
            if (stackItem.item is Tool)
            {
                possibleItems.Add((Tool)stackItem.item);
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


    public void SetCurrentAction(Enums.PossibleActions action)
    {
        CurrentAction = action;
    }

    public void SetActionText(Enums.PossibleActions action)
    {
       // player.SetActionText(actionTexts[(int)action]);
    }

    public bool CheckIfActionIsPossible(Enums.PossibleActions action)
    {
        if (action == Enums.PossibleActions.harvest)
        {
            return true;
        }
        Tool selectedTool = SearchTool(action);
        if (selectedTool == null)
        {
            return false;
        }
        return true;
    }
    
}
