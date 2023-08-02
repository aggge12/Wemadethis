using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    private Dictionary<Vector3Int, Node> grid = new Dictionary<Vector3Int, Node>();

    private class Node
    {
        public Vector3Int Position;
        public Node Parent;
        public int G;
        public int H(Vector3Int targetCell){
            return Mathf.Abs(Position.x - targetCell.x) + Mathf.Abs(Position.y - targetCell.y);
        } 
        public int F(Vector3Int targetCell) {
            return G + H(targetCell);
        }
    }

    public void InitializeGrid(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        Debug.Log(allTiles.Length);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                grid[cellPos] = new Node { Position = cellPos };
            }
        }
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int target)
    {
        List<Vector3Int> path = new List<Vector3Int>();

        if (!grid.ContainsKey(start) || !grid.ContainsKey(target))
        {
            Debug.LogWarning("Start or target cell is not walkable.");
            return new List<Vector3Int>();
        }

        HashSet<Node> openSet = new HashSet<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        Node startNode = grid[start];
        Node targetNode = grid[target];

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = GetLowestFNode(openSet, targetNode);
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                path = RetracePath(startNode, targetNode);
                break;
            }

            foreach (Vector3Int neighborPos in GetNeighbors(currentNode.Position))
            {
                if (!grid.ContainsKey(neighborPos) || closedSet.Contains(grid[neighborPos]))
                {
                    continue;
                }

                Node neighborNode = grid[neighborPos];
                int newG = currentNode.G + 1;

                if (newG < neighborNode.G || !openSet.Contains(neighborNode))
                {
                    neighborNode.G = newG;
                    neighborNode.Parent = currentNode;

                    if (!openSet.Contains(neighborNode))
                    {
                        openSet.Add(neighborNode);
                    }
                }
            }
        }

        return path;
    }

    private List<Vector3Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    private Node GetLowestFNode(HashSet<Node> nodeSet, Node targetNode)
    {
        Node lowestFNode = null;
        foreach (Node node in nodeSet)
        {
            if (lowestFNode == null || node.F(targetNode.Position) < lowestFNode.F(targetNode.Position))
            {
                lowestFNode = node;
            }
        }
        return lowestFNode;
    }

    private List<Vector3Int> GetNeighbors(Vector3Int cellPos)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();
        neighbors.Add(cellPos + new Vector3Int(1, 0, 0));
        neighbors.Add(cellPos + new Vector3Int(-1, 0, 0));
        neighbors.Add(cellPos + new Vector3Int(0, 1, 0));
        neighbors.Add(cellPos + new Vector3Int(0, -1, 0));
        neighbors.Add(cellPos + new Vector3Int(1, -1, 0));
        neighbors.Add(cellPos + new Vector3Int(-1, 1, 0));
        return neighbors;
    }
}
