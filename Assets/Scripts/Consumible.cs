using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : Item
{
    [SerializeField] ConsumibleData consumibleData;
    
    public void Use()
    {
        actionsManager.UseConsumible(this);
    }
}
