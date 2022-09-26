using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] ResourceData resourceData;
    ActionsManager actionsManager;
    public HealthBar healthBar;
    int health;
    int maxHealth;



    void Awake()
    {
        actionsManager = FindObjectOfType<ActionsManager>();
        healthBar.SetMaxHealth(resourceData.MaxHealth);
        health = resourceData.MaxHealth;
        maxHealth = resourceData.MaxHealth;
    }

    void Start() {
        if (health == maxHealth)
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    public void SetHealth(int value)
    {
        health = value;
        healthBar.SetHealth(health);
        if (health != maxHealth)
        {
            healthBar.gameObject.SetActive(true);
        }
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    public int Health { get { return health; } }

    public GameObject RewardPrefab { get { return resourceData.RewardPrefab; } }

    public float SpawnRewardRadius { get { return resourceData.SpawnRewardRadius; } }

    public Enums.PossibleActions RequiredAction { get { return resourceData.RequiredAction; } } 


    void OnMouseDown()
    {
        actionsManager.DispatchAction(this);
    }

}
