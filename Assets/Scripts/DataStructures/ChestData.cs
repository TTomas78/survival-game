
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building/Chest")]


public class ChestData : ScriptableObject
{
    [SerializeField] int slots;

    public int Slots { get => slots; }
}
