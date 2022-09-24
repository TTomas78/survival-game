using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField] int health = 5;
    [SerializeField] int maxHealth = 5;
    [SerializeField] float energy = 100;
    [SerializeField] float maxEnergy = 100;
    [SerializeField] float thirst = 100;
    [SerializeField] float maxThirst = 100;
    [SerializeField] float hunger = 100;
    [SerializeField] float maxHunger = 100;
    [SerializeField] float speed = 5.0f;

    public int Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Energy { get => energy; set => energy = value; }
    public float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }
    public float Thirst { get => thirst; set => thirst = value; }
    public float MaxThirst { get => maxThirst; set => maxThirst = value; }
    public float Hunger { get => hunger; set => hunger = value; }
    public float MaxHunger { get => maxHunger; set => maxHunger = value; }
    public float Speed { get => speed; set => speed = value; }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    public void UseEnergy(float energy)
    {
        this.energy -= energy;
    }

    public void UseThirst(float thirst)
    {
        this.thirst -= thirst;
    }

    public void UseHunger(float hunger)
    {
        this.hunger -= hunger;
    }

    public void RegenerateEnergy(float energy)
    {
        this.energy += energy;
    }

    public void RegenerateThirst(float thirst)
    {
        this.thirst += thirst;
    }

    public void RegenerateHunger(float hunger)
    {
        this.hunger += hunger;
    }

    public void RegenerateHealth(int health)
    {
        this.health += health;
    }

    public void RegenerateAll()
    {
        RegenerateEnergy(1);
        RegenerateThirst(1);
        RegenerateHunger(1);
        RegenerateHealth(1);
    }

    public void UseAll()
    {
        UseEnergy(1);
        UseThirst(1);
        UseHunger(1);
    }

    public void ResetAll()
    {
        health = maxHealth;
        energy = maxEnergy;
        thirst = maxThirst;
        hunger = maxHunger;
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    public void ResetEnergy()
    {
        energy = maxEnergy;
    }

    public void ResetThirst()
    {
        thirst = maxThirst;
    }

    public void ResetHunger()
    {
        hunger = maxHunger;
    }

    public void ResetSpeed()
    {
        speed = 5.0f;
    }

}
