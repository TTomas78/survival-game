using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

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
        destroySlots();
        createSlots(craftingManager.AvailableRecipes(),true);
        createSlots(craftingManager.UnavailableRecipes(), false);
    }

    private void createSlots(List<RecipeData> recipeList, bool available)
    {
        byte multiplier = 120;
        if (available)
            multiplier = 255;
        for (int i = 0; i < recipeList.Count; i++)
        {
            CraftSlot slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponent<Image>().color = new Color32(255, 255, 255, multiplier);
            slot.GetComponent<Button>().interactable = available; 
            slot.AddCraft(recipeList[i]);
            slot.ConfigureUI(this);
        }
    }

    public void CraftItem(RecipeData recipe)
    {
        craftingManager.CraftItem(recipe);
    }

    private void destroySlots()
    {
        foreach (CraftSlot slot in slotParent.GetComponentsInChildren<CraftSlot>())
        {
            DestroyImmediate(slot.gameObject);
        }
    }


}
