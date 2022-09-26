using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _hungerText;
    [SerializeField] private TextMeshProUGUI _thirstText;
    [SerializeField] private TextMeshProUGUI _energyText;

    PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = PlayerStats.instance;
        _playerStats.OnHealthChanged += UpdateHealth;
        _playerStats.OnHungerChanged += UpdateHunger;
        _playerStats.OnThirstChanged += UpdateThirst;
        _playerStats.OnEnergyChanged += UpdateEnergy;

        UpdateHealth(_playerStats.health);
        UpdateHunger(_playerStats.hunger);
        UpdateThirst(_playerStats.thirst);
        UpdateEnergy(_playerStats.energy);

    }

    private void UpdateHealth(int health)
    {
        _healthText.text = health.ToString();
    }

    private void UpdateHunger(int hunger)
    {
        _hungerText.text = hunger.ToString();
    }

    private void UpdateThirst(int thirst)
    {
        _thirstText.text = thirst.ToString();
    }

    private void UpdateEnergy(int energy)
    {
        _energyText.text = energy.ToString();
    }

}
