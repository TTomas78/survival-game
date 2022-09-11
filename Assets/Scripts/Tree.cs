using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] GameObject woodPrefab;
    [SerializeField] float spawnRadius = 2;
    
    [SerializeField] int health = 5;
    [SerializeField] int maxHealth = 5;
    [SerializeField] int woodAmount = 3;

    public HealthBar healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // get the mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // check if the mouse position is inside the collider
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePos))
            {
                Debug.Log("Clicked on the tree");
                if (health >= 1)
                    StartCoroutine(SpawnWoodRoutine());
                if (health < 1)
                    Destroy(gameObject);
            }
        }

  
    }
    
    IEnumerator SpawnWoodRoutine()
    {
        {
            yield return new WaitForSeconds(0.1f);
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Debug.Log(randomPosition);
            Instantiate(woodPrefab, randomPosition, Quaternion.identity);
            health--;
            healthBar.SetHealth(health);
        }
    }
}
