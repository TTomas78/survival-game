
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 5.0f;
    [SerializeField] public float pickUpDistance = 0.5f;
    public Grid grid;
    List<Node> path;
    Pathfinding pathfinding;
    PlayerStats _playerStats;
    [SerializeField] HealthBar actionBarUI;

    private int Health
    {
        get => _playerStats.health;
        set => _playerStats.UpdateHealth(value);
    }
    private int Hunger
    {
        get => _playerStats.hunger;
        set => _playerStats.UpdateHunger(value);
    }
    private int Thirst
    {
        get => _playerStats.thirst;
        set => _playerStats.UpdateThirst(value);
    }
    private int Energy
    {
        get => _playerStats.energy;
        set => _playerStats.UpdateEnergy(value);
    }

    private void Awake()
    {
        _playerStats = PlayerStats.instance;
    }


    void Start()
    {
        pathfinding = grid.GetComponent<Pathfinding>();
        StartCoroutine(hitCollidersDetection());

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
        // get keyboard input
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // set direction
        moveDirection = new Vector3(h, v, 0);

        if (h != 0 || v != 0)
        {
            // activate walking anitation
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);

            // set sprite direction
            if (h > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (h < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }


            // move the player
            transform.position += moveDirection * speed * Time.deltaTime;
        } else 
        {
            gameObject.GetComponent<Animator>().SetBool("isMoving", false);
        }


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
            // start moving animation
            gameObject.GetComponent<Animator>().SetBool("isMoving", true); 
            // flip sprite
            if (path[0].worldPosition.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (path[0].worldPosition.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            Node nextStep = path[0];
            // Debug.Log(nextStep.worldPosition);
            Vector2 nextPosition = new Vector2(nextStep.worldPosition.x, nextStep.worldPosition.y);
            // move the player to the next position
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            // if the player is at the next position, remove it from the path
            if ((Vector2)transform.position == nextPosition)
            {
                path.Remove(nextStep);
            }

            // if the player is at the target position, stop moving
            if (path.Count == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("isMoving", false);
            }
        }
    }

    // update player stats
    public void UpdateDayStats()
    {
        // update player stats
        Health += 1;
        Hunger -= 1;
        Thirst -= 1;
        Energy -= 1;
    }

    // hitColliders detection
    IEnumerator hitCollidersDetection()
    {
        while (true)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, pickUpDistance);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Item")
                {
                    Item item = hitCollider.gameObject.GetComponent<Item>();
                    if (item.pickable)
                    {
                        // Debug.Log("Item in range");
                        item.PickUp();
                    }
                }
            }
            // Debug.Log("hitCollidersDetection");
            yield return new WaitForSeconds(0.1f);
        }

    }

    // draw circle around player
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickUpDistance);
    }
    
    public void MoveTo(Vector3 targetPosition)
    {
        pathfinding.FindPath(transform.position, targetPosition);
    }

    // action bar
    bool isDoingAction;
    int actionTime;
    int actionTimeMax;
    public void StartAction()
    {
        actionBarUI.gameObject.SetActive(true);
        isDoingAction = true;
    }

    public void StopAction()
    {
        actionBarUI.gameObject.SetActive(false);
        actionTime = 0;
        isDoingAction = false;
    }

    public bool IsDoingAction()
    {
        return isDoingAction;
    }

    public void UpdateActionBar(int value, int maxValue)
    {
        if(isDoingAction)
        {
            actionTimeMax = maxValue;
            actionBarUI.SetMaxHealth(maxValue); // TODO: change method name to value
            actionBarUI.SetHealth(actionTime + 1);
            actionTime += 1;
        }
        if(value <= 0)
        {
            StartCoroutine(StopActionAfterDelay(0.5f));
        }
    }

    IEnumerator StopActionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopAction();
    }
}
