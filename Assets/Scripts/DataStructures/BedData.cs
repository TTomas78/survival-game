using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building/Bed")]


public class BedData : ScriptableObject
{
    [SerializeField] int energyRestore;

    public int EnergyRestore { get => energyRestore; }
}
