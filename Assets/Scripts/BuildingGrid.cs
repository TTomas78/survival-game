using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public GameObject buildingSlotPrefab;
    public GameObject buildingPrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;

    private GameObject[,] grid;

    void Start()
    {
        CreateGrid();
    }

    void Update()
    {


    }

    private void CreateGrid()
    {
        grid = new GameObject[gridWidth, gridHeight];
        Vector3 gridOrigin = transform.position;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // create a new building slot
                GameObject buildingSlot = Instantiate(buildingSlotPrefab);
                // set building slot parent
                buildingSlot.transform.parent = transform;
                // set building slot position
                buildingSlot.transform.position = new Vector3(gridOrigin.x + x, gridOrigin.y + y, gridOrigin.z);
                // set building slot name
                buildingSlot.name = "Building Slot " + x + ", " + y;
                // add building slot to grid
                grid[x, y] = buildingSlot;
            }
        }
    }

    public void SetBuildingSlotOccupied(int x, int y, bool occupied)
    {
        // grid[x, y].GetComponent<BuildingSlot>().occupied = occupied;
    }

    public bool IsBuildingSlotOccupied(int x, int y)
    {
        // return grid[x, y].GetComponent<BuildingSlot>().occupied;
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 gridOrigin = transform.position;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Gizmos.DrawWireCube(new Vector3(gridOrigin.x + x, gridOrigin.y + y, gridOrigin.z), Vector3.one);
            }
        }
    }

    void PlaceBuilding(int x, int y)
    {
        if (IsBuildingSlotOccupied(x, y))
        {
            return;
        }
        
        SetBuildingSlotOccupied(x, y, true);
        
        GameObject building = Instantiate(buildingPrefab);
        building.transform.position = new Vector3(x, y, 0);
        
    }

    void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 gridPosition = transform.position;
        int x = Mathf.FloorToInt(mousePosition.x - gridPosition.x);
        int y = Mathf.FloorToInt(mousePosition.y - gridPosition.y);
        PlaceBuilding(x, y);
    }

}

