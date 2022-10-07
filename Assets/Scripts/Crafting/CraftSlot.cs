using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CraftSlot : MonoBehaviour
{
    public Image icon;
    RecipeData recipe;
    CraftingUI craftingUI;

    public void AddCraft(RecipeData newRecipe)
    {
        recipe = newRecipe;
        SpriteRenderer sr = recipe.ResultPrefab.gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(sr.sprite);
        icon.sprite = sr.sprite;
        icon.enabled = true;
    }

    public void ConfigureUI(CraftingUI UI)
    {
        craftingUI = UI;
    }


    public void OnCraftButton()
    {
        craftingUI.CraftItem(recipe);
    }



}
