using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    CraftingManager craftingManager;
    public Transform slotParent;

    [SerializeField] CraftSlot slotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        craftingManager = CraftingManager.instance;
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

    }

    private void createSlots(List<RecipeData> recipeList)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            CraftSlot slot = Instantiate(slotPrefab, slotParent);
            slot.AddCraft(recipeList[i]);
        }
    }
}
