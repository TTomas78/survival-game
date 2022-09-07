using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public EquipmentSlot slot;
    public int resourceGater;
}

public enum EquipmentSlot {Weapon, Armor}