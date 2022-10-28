using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Campfire : Building, IDrageable
{
    [SerializeField] CampfireData data;

    private int remainingFuel;

    private UncookedFood slot;


    // Start is called before the first frame update
    void Start()
    {
        remainingFuel = data.RemainingFuel;
        InvokeRepeating("Use", 0, data.BurningRate);
        slot = null;
    }

    // Update is called once per frame

    private void Use()
    {
        Debug.Log("burning");
        Debug.Log(remainingFuel);
        if (remainingFuel > 0)
        {
            remainingFuel--;
            if (slot is not null)
            {
                slot.DecreaseCookingTime(1);

                if (slot.CookingTime <= 0)
                {
                    Vector3 randomPosition = new Vector3(
                        UnityEngine.Random.Range(-1, 1),
                        UnityEngine.Random.Range(1, 1),
                        0);
                    Instantiate(slot.Food, transform.position + randomPosition, Quaternion.identity);
                    slot = null;
                }
            }
        }
    }

    

    public void AddFood(UncookedFood food)
    {
        slot = food;
    }

    public void AddFuel(Item fuel)
    {
        int value = data.ResourceFuel[fuel.name_id];
        remainingFuel += value;

    }


    public bool OnDropObject(Item droppedItem)
    {
        Debug.Log(droppedItem);
        if (slot == null && droppedItem is UncookedFood )
        {
            UncookedFood food = (UncookedFood)droppedItem;
            AddFood(food);
            return true;
        }
        else if (data.ResourceFuel.ContainsKey(droppedItem.name_id))
        {
            AddFuel(droppedItem);
            return true;
        }
        Debug.Log("wrongly deposited");

        return false;
    }
}
