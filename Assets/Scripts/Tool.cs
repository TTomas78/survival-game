using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    [SerializeField] ToolData data;

    public int Damage { get { return data.Damage; } }
    public List<Enums.PossibleActions> Actions { get { return data.Actions; } }

    public int durability { get { return data.Durability; } }

}
