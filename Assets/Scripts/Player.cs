using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 5.0f;


    // Update is called once per frame
    void Update()
    {
        handleKeyboardMovement();
        handleMouseMovement();
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

    bool moving;
    Vector2 lastClickedPos;
    private void handleMouseMovement()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        }
        else
        {
            moving = false;
        }
    }
}
