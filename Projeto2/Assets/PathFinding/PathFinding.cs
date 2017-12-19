using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    Grid grid;

    private void Start()
    {
        grid = GetComponent<Grid>();

    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        //transformar posiçoes em nodos
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        //Listas de abertos e fechados
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            //passar nodo da lista de abertos para lista fechados
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);


            if (currentNode == targetNode)
            {
                return;
            }

            //loop nos vizinhos
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    //este comando continua o loop
                    continue;
                }
            }
        }
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        //tornar em valores absulutos/ transforma numeros negativos em positivos/ -5 = 5
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            //??????
            return 14 * distY + 10 * (distX - distY);
        }
        //????
        return 14 * distX + 10 * (distY - distX);
    }

    // EP03 min 16:46
}
