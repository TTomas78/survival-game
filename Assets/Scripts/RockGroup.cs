using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGroup : MonoBehaviour
{
    [SerializeField] GameObject rockPrefab;
    [SerializeField] float spawnRadius = 2;
    [SerializeField] int health = 5;

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
                Debug.Log("Clicked on the rock group");
                if (health >= 1)
                    StartCoroutine(SpawnRockRoutine());
                if (health < 1)
                    Destroy(gameObject);
            }
        }
    }

    IEnumerator SpawnRockRoutine()
    {
        {
            yield return new WaitForSeconds(0.1f);
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Instantiate(rockPrefab, randomPosition, Quaternion.identity);
            health--;
        }
    }
}
