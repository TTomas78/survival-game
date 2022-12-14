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

}
