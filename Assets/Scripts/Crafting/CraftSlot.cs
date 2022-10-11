using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using TMPro;

public class CraftSlot : MonoBehaviour
{
    public Image icon;
    RecipeData recipe;

    [SerializeField] TMP_Text nameTxt;
    [SerializeField] TMP_Text requerimentTxt;
    [SerializeField] Transform requirementParent;
    [SerializeField] TMP_Text availableTxt;

    public void AddCraft(RecipeData newRecipe, bool available)
    {
        recipe = newRecipe;
        SpriteRenderer sr = recipe.ResultPrefab.gameObject.GetComponent<SpriteRenderer>();
        icon.sprite = sr.sprite;
        icon.enabled = true;
        nameTxt.text = recipe.ResultPrefab.name_id;
        availableTxt.text = available ? "Available" : "Unavailable";
        availableTxt.color = available ? Color.green : Color.red;
        foreach (Item item in recipe.RecipeComponents)
        {
            TMP_Text text = Instantiate(requerimentTxt, requirementParent);
            text.text = item.name_id;
            // text.color = InventoryManager.instance.HasItem(item) ? Color.green : Color.red;
        }
    }

    public void OnCraftButton()
    {
        Debug.Log("Crafting " + recipe.ResultPrefab.name_id);
        CraftingManager.instance.CraftItem(recipe);
    }

    public void UpdateUI()
    {
        if (CraftingManager.instance.IsAbleToCraft(recipe))
        {
            availableTxt.text = "Available";
            availableTxt.color = Color.green;
        }
        else
        {
            availableTxt.text = "Unavailable";
            availableTxt.color = Color.red;
        }
    }

}
