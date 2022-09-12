using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadious;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadious * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 WorldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadious) + Vector3.up * (y * nodeDiameter + nodeRadious);
                Vector2 box = new Vector2(nodeRadious, nodeRadious);
                bool walkable = !(Physics2D.OverlapBox(WorldPoint, box, 90,unwalkableMask));
                grid[x, y] = new Node(walkable, WorldPoint);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        Debug.Log("worldpositionx");
        Debug.Log(worldPosition.x);

        Debug.Log("gridworld size");
        Debug.Log(gridWorldSize.x);

        Debug.Log("porcentaje");
        Debug.Log(percentX);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        Debug.Log("clamp");
        Debug.Log(percentX);

        int x = Mathf.RoundToInt((gridSizeX-1)*percentX);
        int y = Mathf.RoundToInt((gridSizeY-1)*percentY);
        Debug.Log("gridsize");
        Debug.Log(gridSizeX);

        Debug.Log("nodo");
        Debug.Log(x);

        return grid[x, y];
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));

        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (playerNode == node)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }

    }
}

