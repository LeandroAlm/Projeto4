using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Transform player;
    Node[,] grid;
    public Vector2 gridWorlSize;
    public float nodeRadius;
    public LayerMask unWalkableMask;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    bool isPlayer;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorlSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorlSize.y / nodeDiameter);
        CreateGrid();

        isPlayer = false;
    }
  

 

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorlSize.x/2 - Vector3.forward * gridWorlSize.y / 2;

        for (int X = 0; X < gridSizeX; X++)
        {
            for (int Y = 0; Y < gridSizeY; Y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (X * nodeDiameter + nodeRadius) + Vector3.forward * (Y * nodeDiameter * nodeRadius);

                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));

                grid[X, Y] = new Node(walkable, worldPoint);


            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        if (!isPlayer)
        {
            float percentX = (worldPosition.x + gridWorlSize.x / 2) / gridWorlSize.x;
            float percentY = (worldPosition.z + gridWorlSize.y / 2) / gridWorlSize.y;

            //ee

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            Node xpto = grid[x, y];

            Debug.Log("xpto: " + xpto.worldPos);


            return grid[x, y];
        }
        else
        {
            return grid[Mathf.RoundToInt(worldPosition.x) / 2, Mathf.RoundToInt(worldPosition.z) / 2];
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorlSize.x, 1, gridWorlSize.y));

    //    if (grid != null)
    //    {
    //        Node playerNode = NodeFromWorldPoint(player.position);

    //        Debug.Log("NodePlayer: " + playerNode.worldPos);
    //        Debug.Log("PosPlayer: " + player.position);
    //        foreach (Node n in grid)
    //        {
    //            Gizmos.color = (n.walkable) ? Color.white : Color.red;
    //            if (playerNode == n)
    //            {
    //                isPlayer = true;
    //                Gizmos.color = Color.cyan;
    //            }
    //            Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - 0.1f));
    //        }


    //    }
    //}

}
