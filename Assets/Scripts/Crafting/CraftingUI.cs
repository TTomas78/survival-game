using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using TMPro;


public class CraftingUI : MonoBehaviour
{
    CraftingManager craftingManager;
    public Transform slotParent;
    [SerializeField] CraftSlot slotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        craftingManager = CraftingManager.instance;

        craftingManager.onRecipeChangedCallback += UpdateUI;
        InventoryManager.instance.onItemChangedCallback += UpdateUI;
        UpdateUI();

    }

    //we should update the ui once the method of the crafting manager is dispatched. also it should be updated once the inventory is open
    public void UpdateUI()
    {
        DestroySlots();
        CreateSlots(craftingManager.AvailableRecipes(), true);
        CreateSlots(craftingManager.UnavailableRecipes(), false);
    }

    private void CreateSlots(List<RecipeData> recipeList, bool available)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            CraftSlot slot = Instantiate(slotPrefab, slotParent);
            slot.AddCraft(recipeList[i], available);
        }
    }

    private void DestroySlots()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnCloseButton()
    {
        gameObject.SetActive(false);
    }

}
