using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
   public bool occupied = false;
   public int[] gridPosition = new int[2];
   private BuildingGrid buildingGrid;

    void OnMouseDown()
    {
        Debug.Log("Clicked on building slot " + gridPosition[0] + ", " + gridPosition[1]);
        // get grid
        buildingGrid = GameObject.Find("BuildingGrid").GetComponent<BuildingGrid>();
        // call building grid to place building
        buildingGrid.PlaceBuilding(gridPosition[0], gridPosition[1]);
    }
    
}
