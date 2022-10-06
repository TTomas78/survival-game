using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    CraftingManager craftingManager;
    public Transform slotParent;

    CraftSlot[] slots;

    CraftingUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of the craftingUI found!");
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        craftingManager = CraftingManager.instance;
        slots = slotParent.GetComponentsInChildren<CraftSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            UpdateUI();
        }
    }

    //we should update the ui once the method of the crafting manager is dispatched. also it should be updated once the inventory is open
    public void UpdateUI()
    {
        createSlots(craftingManager.AvailableRecipes());
        createSlots(craftingManager.UnavailableRecipes());
        Debug.Log(slots.Length);

    }

    private void createSlots(List<RecipeData> recipeList)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            Debug.Log("entro aca");
            CraftSlot newCraft = new CraftSlot(recipeList[i]);
            newCraft.transform.parent = slotParent;
            slots.Append(newCraft);
        }
    }
}
