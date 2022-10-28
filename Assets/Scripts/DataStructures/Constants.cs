using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A class to consume the same enums globally
public class Enums : MonoBehaviour
{
    public enum PossibleActions
    {
        chop,
        mine,
        harvest,
        fish
    }
}

public class Dicts : MonoBehaviour
{
    public IDictionary Fuels = new Dictionary<string, int>(){
        {"Wood", 2},
        {"Stick", 1}
    };
}

    

