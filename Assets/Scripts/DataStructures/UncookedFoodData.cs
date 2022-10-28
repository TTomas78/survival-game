using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UncookedFood", menuName = "Uncooked Food")]

public class UncookedFoodData : ScriptableObject
{
    [SerializeField] int cookingTime;
    [SerializeField] GameObject cookingResultPrefab;
    // Start is called before the first frame update
    public int CookingTime { get => cookingTime; }

    public GameObject CookingResultPrefab { get => cookingResultPrefab; }
}

