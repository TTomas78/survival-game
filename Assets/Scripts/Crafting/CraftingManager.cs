using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;
    InventoryManager inventory;
    [SerializeField] List<RecipeData> unlockedRecipes;
    [SerializeField] List<RecipeData> lockedRecipes;
    public List<RecipeData> CraftedRecipes;
    [SerializeField] GameObject craftingUI;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            craftingUI.SetActive(!craftingUI.activeSelf);
        }
    }

    // onRecipeChangedCallback
    public delegate void OnRecipeChanged();
    public OnRecipeChanged onRecipeChangedCallback;

    // Unlock new recipes when the requisites are completed
    public void UnlockRecipes()
    {
        foreach (RecipeData recipe in lockedRecipes)
        {
            if (recipe.IsUnlocked())
            {
                unlockedRecipes.Add(recipe);
                lockedRecipes.Remove(recipe);
                onRecipeChangedCallback.Invoke();
                return;
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
            foreach (Item item in recipe.RecipeComponents)
            {
                // search for the item in the inventory
                StackItem stackItem = inventory.SearchItemByName(item.name_id);
                if (stackItem != null)
                {
                    // substract the resources from the resourceGatherer
                    int quantity = recipe.RecipeComponentsQuantity[recipe.RecipeComponents.IndexOf(item)];
                    inventory.SubtractItemByName(item.name_id, quantity);
                }
            }
            inventory.Add(recipe.ResultPrefab);
        }
        if (!CraftedRecipes.Contains(recipe))
        {
            CraftedRecipes.Add(recipe);
            UnlockRecipes();
        }
    }

    public bool IsAbleToCraft(RecipeData recipe)
    {
        foreach (Item item in recipe.RecipeComponents)
        {
            // check if the item is in the inventory
            StackItem stackItem = inventory.SearchItemByName(item.name_id);
            if (stackItem == null)
                return false;
            
            // check if the player has enough resources
            int currentQuantity = stackItem.quantity;
            int requiredQuantity = recipe.RecipeComponentsQuantity[recipe.RecipeComponents.IndexOf(item)];
            if (currentQuantity < requiredQuantity)
                return false;
            
        }
        return true;
    }
}
