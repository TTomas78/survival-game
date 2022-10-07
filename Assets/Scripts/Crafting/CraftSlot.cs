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

    public void AddCraft(RecipeData newRecipe)
    {
        recipe = newRecipe;
        SpriteRenderer sr = recipe.ResultPrefab.gameObject.GetComponent<SpriteRenderer>();
        Debug.Log(sr.sprite);
        icon.sprite = sr.sprite;
        icon.enabled = true;
    }

    //maybe this method shouldn't be neccesary
    public void CleanSlot()
    {
        recipe = null;
        icon.sprite = null;
        icon.enabled=false;
    }


}
