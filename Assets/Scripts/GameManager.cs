using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 pointer;
    public GameObject imagePointer;

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        pointer = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        imagePointer.transform.position = new Vector2(pointer.x, pointer.y);

    }
    
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
