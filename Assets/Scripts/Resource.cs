using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] ResourceData resourceData;
    ActionsManager actionsManager;
    Player _player;
    bool playerInRange;
    bool interactuable;
    int health;
    int maxHealth;

    void Awake()
    {
        actionsManager = FindObjectOfType<ActionsManager>();
        _player = FindObjectOfType<Player>();

        health = resourceData.MaxHealth;
        maxHealth = resourceData.MaxHealth;
    }

    public void SetHealth(int value)
    {
        health = value;
        // TODO: _player.Energy -= resourceData.EnergyCost;
        _player.UpdateActionBar(health, maxHealth);

    }

    public int Health { get { return health; } }

    public GameObject RewardPrefab { get { return resourceData.RewardPrefab; } }

    public float SpawnRewardRadius { get { return resourceData.SpawnRewardRadius; } }

    public Enums.PossibleActions RequiredAction { get { return resourceData.RequiredAction; } } 

    public void Interact()
    {
        StartCoroutine(InteractCoroutine());
    }

    void OnMouseEnter()
    {
        //TODO: actionsManager.SetActionText(resourceData.RequiredAction);
    }

    void OnMouseExit()
    {
        //TODO: actionsManager.SetActionText(Enums.PossibleActions.None);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(IsInteractuable())
            {
                if(!IsPlayerInRange())
                {
                    //  move the player to the resource
                    _player.MoveTo(transform.position);
                }
                // set the current action to the required action
                actionsManager.SetCurrentAction(resourceData.RequiredAction);
                actionsManager.DispatchAction(this);
            }
        }
    }

    IEnumerator SpawnRewardItems()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < resourceData.RewardAmount; i++)
        {
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-resourceData.SpawnRewardRadius, resourceData.SpawnRewardRadius),
                UnityEngine.Random.Range(-resourceData.SpawnRewardRadius, resourceData.SpawnRewardRadius),
                0);
            Instantiate(resourceData.RewardPrefab, transform.position + randomPosition, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    IEnumerator InteractCoroutine()
    {
        yield return new WaitUntil(() => IsPlayerInRange());

        while (true)
        {
            if (actionsManager.CurrentAction == RequiredAction)
            {
                SetHealth(Health - 1);
            }
            if (Health == 0)
            {
                StartCoroutine(SpawnRewardItems());
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, _player.transform.position);
    }

    private bool IsPlayerInRange()
    {
        return  GetDistanceToPlayer() <= 1.5f;
    }

    // check if the resource is interactuable
    private bool IsInteractuable()
    {
        return actionsManager.CheckIfActionIsPossible(RequiredAction);
    }
}
