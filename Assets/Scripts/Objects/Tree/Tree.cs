using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] ResourceData resourceData;

    [SerializeField] float spawnRadius = 2;

    public HealthBar healthBar;
    int health;
    int maxHealth;

    

    void Awake()
    {
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
    
    void OnClickRoutine()
    {
        {
            StartCoroutine(handleItemSpawn());

            health--;
            healthBar.SetHealth(health);

            if(health != maxHealth)
            {
                healthBar.gameObject.SetActive(true);
            }

            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator handleItemSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject item = Instantiate(resourceData.RewardPrefab, transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        item.GetComponent<Item>().name_id = resourceData.RewardPrefab.name;
    }

    void OnMouseDown()
    {
        OnClickRoutine();
    }

}
