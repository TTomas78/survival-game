using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        //Check if instance already exists
        if (Instance == null)
            //if not, set instance to this
            Instance = this;

    }

    // day/night cycle
    public float dayLength = 10.0f;
    public float dayProgress = 0.0f;
    public int day = 1;
    public bool isDay = true;
    public bool isNight = false;

    // player
    Player player;

    // start 
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // day/night cycle
        dayProgress += Time.deltaTime / dayLength;
        if (dayProgress >= 1.0f)
        {
            Debug.Log(dayProgress);
            dayProgress = 0.0f;
            isDay = !isDay;
            isNight = !isNight;
            day++;
            
            Debug.Log(player);
            // update player stats
            player.UpdateDayStats();
        }


    }

}
