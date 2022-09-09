using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    float h;
    float v;
    Vector3 moveDirection;
    [SerializeField] public float speed = 5.0f;


    // Update is called once per frame
    void Update()
    {
        // get keyboard imput
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // set direction
        moveDirection = new Vector3(h, v, 0);

        // move the player
        transform.position += moveDirection * speed * Time.deltaTime;


    }
}
