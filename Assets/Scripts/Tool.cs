using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    [SerializeField] ToolData data;
    int durability;

    private void Awake()
    {
        durability = data.Durability;
    }

    public int Damage { get { return data.Damage; } }
    public List<Enums.PossibleActions> Actions { get { return data.Actions; } }

    public int Durability { get => durability; }

    public void decreaseDurability(int value)
    {
        durability -= value;

        //code to update the health bar
    }

}
