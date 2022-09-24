using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Item/Tool")]


public class ToolData : ScriptableObject
{
    [SerializeField] List<Enums.PossibleActions> actions;
    [SerializeField] int damage;
    [SerializeField] int durability;

    public List<Enums.PossibleActions> Actions { get => actions;}

    public int Damage { get => damage;}

    public int Durability { get => durability; set => durability = value; }
}
