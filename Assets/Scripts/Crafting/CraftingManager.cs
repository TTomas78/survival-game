using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;
    InventoryManager inventory;
    [SerializeField] List<RecipeData> unlockedRecipes;
    [SerializeField] List<RecipeData> lockedRecipes;
    [SerializeField] List<RecipeData> CraftedRecipes;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryManager.instance;
    }

    // Unlock new recipes when the requisites are completed
    public void UnlockRecipes()
    {
        for (int i = 0; i < lockedRecipes.Count; i++)
        {
            List<RecipeData> requisites = lockedRecipes[i].Requisites;
            
            for (int j = 0; j < requisites.Count; j++) {
                if (!CraftedRecipes.Contains(requisites[j])){
                    break;
                }
                else
                {
                    if(j == requisites.Count - 1)
                    {
                        unlockedRecipes.Add(lockedRecipes[i]);
                        lockedRecipes.RemoveAt(i);
                    }
                }

            }
        }
    }

    //Get the recipes that the player is able to craft based on the componentes in the inventory
    public List<RecipeData> AvailableRecipes()
    {
        List<RecipeData> recipes = new List<RecipeData>();
        foreach(RecipeData recipe in unlockedRecipes)
        {
            if (IsAbleToCraft(recipe))
                recipes.Add(recipe);
        }
        return recipes;
    }

    //Get the recipes that the player is not able to craft based on the componentes in the inventory
    public List<RecipeData> UnavailableRecipes()
    {
        List<RecipeData> recipes = new List<RecipeData>();
        foreach (RecipeData recipe in unlockedRecipes)
        {
            if (!IsAbleToCraft(recipe))
                recipes.Add(recipe);
        }
        return recipes;
    }

    //Craft an item and substract the resources from the inventory
    public void CraftItem(RecipeData recipe)
    {
        if (IsAbleToCraft(recipe))
        {
            for (int i = 0; i < recipe.RecipeComponents.Count; i++)
            {

                string objectName = recipe.RecipeComponents[i].name_id;
                int index = -1;
                for (int j = 0; j < inventory.items.Count; j++)
                {
                    if (inventory.items[j].name_id == objectName)
                    {
                        index = j;
                    }
                }
                inventory.items[index].resourceGater = inventory.items[index].resourceGater - recipe.RecipeComponentsQuantity[i];
                if (inventory.items[index].resourceGater == 0)
                    inventory.items[index].RemoveFromInventory();
                inventory.Add(recipe.ResultPrefab);
            }
        }
        else
        {
            Debug.Log("something went wrong");
        }
        if (!CraftedRecipes.Contains(recipe))
        {
            CraftedRecipes.Add(recipe);
            UnlockRecipes();
        }
    }

    // Return true if a recipe is able to be crafted
    public bool IsAbleToCraft(RecipeData recipe)
    {
        for(int i = 0; i < recipe.RecipeComponents.Count; i++)
        {
            string objectName = recipe.RecipeComponents[i].name_id;
            int index = -1;
            for (int j = 0; j < inventory.items.Count; j++)
            {
                if (inventory.items[j].name_id == objectName)
                {
                    index = j;
                }
            }
            if (index != -1)
            {
                if (recipe.RecipeComponentsQuantity[i] > inventory.items[index].resourceGater)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}
