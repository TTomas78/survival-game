using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumible", menuName = "Consumible")]


public class ConsumibleData : ScriptableObject
{
    [SerializeField] int hungerRestore;
    [SerializeField] int thirstRestore;
    [SerializeField] int healthRestore;

    public int HungerRestore { get => hungerRestore; }
    public int ThirstRestore { get => thirstRestore; }
    public int HealthRestore { get => healthRestore; }
}
