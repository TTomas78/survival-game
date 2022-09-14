using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 5.0f;
    public Grid grid;
    List<Node> path;
    Pathfinding pathfinding;
    private void Awake()
    {
        pathfinding = grid.GetComponent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        handleKeyboardMovement();
        handleMouseMovement();
        MovePlayer();

    }
    
    float h;
    float v;
    Vector3 moveDirection;
    private void handleKeyboardMovement()
    {
        // get keyboard imput
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // set direction
        moveDirection = new Vector3(h, v, 0);

        // move the player
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void handleMouseMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = transform.position;

            pathfinding.FindPath(playerPosition, targetPosition);

        }
    }

    private void MovePlayer()
    {
        path = grid.gameObject.GetComponent<Grid>().path;
        if (path.Count != 0)
        {
            Node nextStep = path[0];
            Debug.Log(nextStep.worldPosition);
            Vector2 nextPosition = new Vector2(nextStep.worldPosition.x, nextStep.worldPosition.y);
            // move the player to the next position
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            if ((Vector2)transform.position == nextPosition)
            {
                path.Remove(nextStep);
            }
        }
    }
}
