using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListManager : MonoBehaviour
{
    public static ItemListManager instance;
    [SerializeField] public List<Item> items = new List<Item>();
    [SerializeField] GameObject ItemListUI;
    [SerializeField] GameObject[] ItemPrefabs;

    void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one instance of itemList found!");
                return;
            }
            instance = this;
        }

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    // Start is called before the first frame update
    void Start()
    {
        // iterate ItemsPrefabs and save them in items
        foreach (GameObject item in ItemPrefabs)
        {
            items.Add(item.GetComponent<Item>());
        }
        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        
    }

    // Update is called once per frame
    void Update()
    {
        // on press e open itemList
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(ItemListUI.activeSelf)
            {
                ItemListUI.SetActive(false);
                return;
            }
            ItemListUI.SetActive(true);
          
            
        }
    }

    // spawn item
    public void SpawnItem(Item item)
    {
        // search item name_id in items
        foreach (Item i in items)
        {
            if (i.name_id == item.name_id)
            {
                // spawn item
                Instantiate(i, transform.position, Quaternion.identity);
                return;
            }
        }
    }
}
