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
                // set gridPosition in BuildingSlot script
                buildingSlot.gameObject.GetComponent<BuildingSlot>().gridPosition = new int[2] { x, y };
                // set building slot name
                buildingSlot.name = "Building Slot " + x + ", " + y;
                // add building slot to grid
                grid[x, y] = buildingSlot;
            }
        }
    }

    public void SetBuildingSlotOccupied(int x, int y, bool occupied)
    {
        grid[x, y].GetComponent<BuildingSlot>().occupied = occupied;
    }

    public bool IsBuildingSlotOccupied(int x, int y)
    {
        Debug.Log("Is building slot " + x + ", " + y + " occupied? " + grid[x, y].GetComponent<BuildingSlot>().occupied);
        return grid[x, y].GetComponent<BuildingSlot>().occupied;
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

    public void PlaceBuilding(int x, int y)
    {
        if (IsBuildingSlotOccupied(x, y))
        {
            return;
        }
        
        SetBuildingSlotOccupied(x, y, true);
        
        GameObject building = Instantiate(buildingPrefab);
        building.transform.parent = transform;
        building.transform.position = new Vector3(
            grid[x, y].transform.position.x,
            grid[x, y].transform.position.y,
            grid[x, y].transform.position.z
            );
    }

}

