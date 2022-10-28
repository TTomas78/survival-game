using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncookedFood : Item
{
    int cookingTime;
    [SerializeField] UncookedFoodData food;

    // Start is called before the first frame update
    void Start()
    {
        cookingTime = food.CookingTime;    
    }

    public void DecreaseCookingTime(int time)
    {
        cookingTime -= time;
    }

    public int CookingTime { get { return cookingTime; } }    
    public GameObject Food { get { return food.CookingResultPrefab; } }
}
