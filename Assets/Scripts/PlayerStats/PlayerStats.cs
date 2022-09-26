using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public delegate void OnHealthChangedDelegate(int health);
    public OnHealthChangedDelegate OnHealthChanged;

    public delegate void OnHungerChangedDelegate(int hunger);
    public OnHungerChangedDelegate OnHungerChanged;

    public delegate void OnThirstChangedDelegate(int thirst);
    public OnThirstChangedDelegate OnThirstChanged;

    public delegate void OnEnergyChangedDelegate(int energy);
    public OnEnergyChangedDelegate OnEnergyChanged;

    public int maxHealth = 100;
    public int maxHunger = 100;
    public int maxThirst = 100;
    public int maxEnergy = 100;

    public int health;
    public int hunger;
    public int thirst;
    public int energy;

    private void Awake()
    {
        instance = this;
        health = maxHealth;
        hunger = maxHunger;
        thirst = maxThirst;
        energy = maxEnergy;
    }

    public void UpdateHealth(int health)
    {
        this.health = health;
        OnHealthChanged?.Invoke(health);
    }

    public void UpdateHunger(int hunger)
    {
        this.hunger = hunger;
        OnHungerChanged?.Invoke(hunger);
    }

    public void UpdateThirst(int thirst)
    {
        this.thirst = thirst;
        OnThirstChanged?.Invoke(thirst);
    }

    public void UpdateEnergy(int energy)
    {
        this.energy = energy;
        OnEnergyChanged?.Invoke(energy);
    }
}
