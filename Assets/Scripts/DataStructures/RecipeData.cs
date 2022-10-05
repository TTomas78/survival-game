using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]

public class RecipeData : ScriptableObject
{
    [SerializeField] List<Item> recipeComponents;
    [SerializeField] List<int> recipeComponentsQuantity;
    [SerializeField] GameObject resultPrefab;
    [SerializeField] List<RecipeData> requisites;

    public List<Item> RecipeComponents { get => recipeComponents; }

    public List<int> RecipeComponentsQuantity { get => recipeComponentsQuantity; }

    public List<RecipeData> Requisites { get => requisites; }

    public GameObject ResultPrefab { get => resultPrefab; }
}

