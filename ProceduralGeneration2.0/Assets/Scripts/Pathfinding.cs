using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end, int[,] grid)
    {
        PathNode startNode = new PathNode(start.x, start.y, null, 0, 0);
        PathNode endNode = new PathNode(end.x, end.y, null, 0, 0);

        List<PathNode> openList = new List<PathNode> { startNode };
        List<PathNode> closedList = new List<PathNode>();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            // If reached the end node
            if (currentNode.X == end.x && currentNode.Y == end.y)
            {
                return ReconstructPath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbor in GetNeighbors(currentNode, grid, openList, closedList))
            {
                if (closedList.Contains(neighbor)) continue;

                int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbor);
                if (!openList.Contains(neighbor) || tentativeGCost < neighbor.GCost)
                {
                    neighbor.Parent = currentNode;
                    neighbor.GCost = tentativeGCost;
                    neighbor.HCost = CalculateDistanceCost(neighbor, endNode);

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        // No path found
        return null;
    }

    private List<Vector2Int> ReconstructPath(PathNode endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        PathNode currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(new Vector2Int(currentNode.X, currentNode.Y));
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode current, PathNode neighbor)
    {
        int xDistance = Mathf.Abs(current.X - neighbor.X);
        int yDistance = Mathf.Abs(current.Y - neighbor.Y);

        // Add a penalty for diagonal moves
        if (xDistance != 0 && yDistance != 0) // It's a diagonal move
        {
            return MOVE_DIAGONAL_COST + MOVE_STRAIGHT_COST; // Add a penalty
        }
        return MOVE_STRAIGHT_COST; // Straight move cost
    }

    private PathNode GetLowestFCostNode(List<PathNode> openList)
    {
        PathNode lowestNode = openList[0];
        foreach (PathNode node in openList)
        {
            if (node.FCost < lowestNode.FCost || (node.FCost == lowestNode.FCost && node.HCost < lowestNode.HCost))
            {
                lowestNode = node;
            }
        }
        return lowestNode;
    }

    private List<PathNode> GetNeighbors(PathNode node, int[,] grid, List<PathNode> openList, List<PathNode> closedList)
    {
        List<PathNode> neighbors = new List<PathNode>();
        int x = node.X;
        int y = node.Y;

        Vector2Int[] directions = new[]
        {
            new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };

        foreach (Vector2Int direction in directions)
        {
            int neighborX = x + direction.x;
            int neighborY = y + direction.y;

            if (neighborX >= 0 && neighborX < grid.GetLength(0) &&
                neighborY >= 0 && neighborY < grid.GetLength(1) &&
                grid[neighborX, neighborY] == 1) // 1 = walkable
            {
                // Diagonal move validation
                if (direction.x != 0 && direction.y != 0)
                {
                    if (grid[x + direction.x, y] == 0 || grid[x, y + direction.y] == 0)
                        continue;
                }

                if (!openList.Any(n => n.X == neighborX && n.Y == neighborY) &&
                    !closedList.Any(n => n.X == neighborX && n.Y == neighborY))
                {
                    neighbors.Add(new PathNode(neighborX, neighborY, null, int.MaxValue, int.MaxValue));
                }
            }
        }
        return neighbors;
    }

    public class PathNode
    {
        public int X { get; }
        public int Y { get; }
        public PathNode Parent { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;

        public PathNode(int x, int y, PathNode parent, int gCost, int hCost)
        {
            X = x;
            Y = y;
            Parent = parent;
            GCost = gCost;
            HCost = hCost;
        }
    }
}
