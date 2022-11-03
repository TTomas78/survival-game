 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building/Campfire")]


public class CampfireData : ScriptableObject
{
    [SerializeField] int remainingFuel;
    [SerializeField] float burningRate;

    Dictionary<string, int> resourceFuel = new Dictionary<string, int>(){
        {"Wood", 5},
        {"Stick", 2}
        };


    public int RemainingFuel { get => remainingFuel; }
    public float BurningRate { get => burningRate; }

    public Dictionary<string, int> ResourceFuel { get => resourceFuel; }
}
