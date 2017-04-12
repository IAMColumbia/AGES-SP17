using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GridMaster;

namespace Pathfinding
{
    public class Pathfinder
    {
        GridBase gridBase;

        public Node startPosition;
        public Node endPosition;

        public volatile bool jobDone = false;
        PathfindMaster.PathfindingJobComplete completeCallback;
        List<Node> foundPath;

        public Pathfinder(Node start, Node target, PathfindMaster.PathfindingJobComplete callback)
        {
            startPosition = start;
            endPosition = target;
            completeCallback = callback;
            gridBase = GridBase.Instance;
        }

        public void NotifyComplete()
        {
            if (completeCallback != null)
            {
                completeCallback(foundPath);
            }
        }

        public void FindPath()
        {
            foundPath = ActualFindPath(startPosition, endPosition);

            jobDone = true;
        }

        List<Node> ActualFindPath(Node start, Node target)
        {
            List<Node> foundPath = new List<Node>();

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            // Add start node to the open set
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                for (int i = 0; i < openSet.Count; i++)
                {
                    // Check costs for current node
                    if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                    {
                        // Assign a new current node
                        if (!currentNode.Equals(openSet[i]))
                        {
                            currentNode = openSet[i];
                        }
                    }
                }

                // Remove current node from the open set and add to closed set
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // Check if the current node is the target node
                if (currentNode.Equals(target))
                {
                    foundPath = RetracePath(start, currentNode);
                }

                // If not, look at neighboring nodes
                foreach (Node neighbor in GetNeighbors(currentNode, true))
                {
                    if (!closedSet.Contains(neighbor))
                    {
                        // Create movement cost for neighbors
                        float newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);

                        // Check if it's lower than neighbor's cost
                        if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                        {
                            // Calculate the new movement costs
                            neighbor.GCost = newMovementCostToNeighbor;
                            neighbor.HCost = GetDistance(neighbor, target);

                            // Assign the parent node
                            neighbor.parentNode = currentNode;

                            // Add the neighbor node to the open set
                            if (!openSet.Contains(neighbor))
                            {
                                openSet.Add(neighbor);
                            }
                        }
                    }
                }
            }

            return foundPath;
        }

        List<Node> RetracePath(Node startNode, Node endNode)
        {
            // Go from the end node to the start node to build the path

            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);

                // Get previous node in path with the parent nodes
                currentNode = currentNode.parentNode;
            }

            // Reverse the list to get the correct path
            path.Reverse();

            return path;
        }

        List<Node> GetNeighbors(Node node, bool getVerticalNeighbors)
        {
            List<Node> returnList = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int yIndex = -1; yIndex <= 1; yIndex++)
                {
                    for (int z = -1; z < 1; z++)
                    {
                        int y = yIndex;

                        // If vertical movement isn't present
                        if (!getVerticalNeighbors)
                        {
                            y = 0;
                        }

                        if (x == 0 && y == 0 && z == 0)
                        {
                            // At the current node
                        }
                        else
                        {
                            Node searchPosition = new Node();

                            searchPosition.xPosition = node.xPosition + x;
                            searchPosition.yPosition = node.yPosition + y;
                            searchPosition.zPosition = node.zPosition + z;

                            Node newNode = GetNeighborNode(node, searchPosition, true);

                            if (newNode != null)
                            {
                                returnList.Add(newNode);
                            }
                        }
                    }
                }
            }

            return returnList;
        }

        Node GetNeighborNode(Node currentNodePosition, Node adjacentPosition, bool searchTopDown)
        {
            // Place to add all the checks for neighboring nodes

            Node returnNode = null;

            Node node = GetNode(adjacentPosition.xPosition, adjacentPosition.yPosition, adjacentPosition.zPosition);

            // Check that it's not null and is walkable
            if (node != null && node.isWalkable)
            {
                returnNode = node;
            }
            // If vertical movement is present
            else if (searchTopDown)
            {
                // Look at what's below adjacent node
                adjacentPosition.yPosition -= 1;
                Node bottomBlock = GetNode(adjacentPosition.xPosition, adjacentPosition.yPosition, adjacentPosition.zPosition);

                // Check that it's not null and is walkable
                if (bottomBlock != null && bottomBlock.isWalkable)
                {
                    returnNode = bottomBlock;
                }
                else
                {
                    // Look at what's above adjacent node (+= 2 because currently below adjacent node)
                    adjacentPosition.yPosition += 2;
                    Node topBlock = GetNode(adjacentPosition.xPosition, adjacentPosition.yPosition, adjacentPosition.zPosition);

                    // Check that it's not null and is walkable
                    if (topBlock != null && topBlock.isWalkable)
                    {
                        returnNode = topBlock;
                    }
                }
            }

            // If the node is diagonal check the neighboring nodes as well
            int originalXPosition = adjacentPosition.xPosition - currentNodePosition.xPosition;
            int originalZPosition = adjacentPosition.zPosition - currentNodePosition.zPosition;

            if (Mathf.Abs(originalXPosition) == 1 && Mathf.Abs(originalZPosition) == 1)
            {
                // Neighbor node (originalXPosition, 0)
                Node neighbor1 = GetNode(currentNodePosition.xPosition + originalXPosition, currentNodePosition.yPosition, currentNodePosition.zPosition);
                if (neighbor1 == null || !neighbor1.isWalkable)
                {
                    returnNode = null;
                }

                // Neighbor node (0, originalZPosition)
                Node neighbor2 = GetNode(currentNodePosition.xPosition, currentNodePosition.yPosition, currentNodePosition.zPosition + originalZPosition);
                if (neighbor2 == null || !neighbor2.isWalkable)
                {
                    returnNode = null;
                }
            }

            return returnNode;
        }

        Node GetNode(int x, int y, int z)
        {
            Node n = null;

            lock(gridBase)
            {
                n = gridBase.GetNode(x, y, z);
            }

            return n;
        }

        int GetDistance(Node a, Node b)
        {
            int xDistance = Mathf.Abs(a.xPosition - b.xPosition);
            int yDistance = Mathf.Abs(a.yPosition - b.yPosition);
            int zDistance = Mathf.Abs(a.zPosition - b.zPosition);

            if (xDistance > zDistance)
            {
                return ((14 * zDistance) + (10 * (xDistance - zDistance)) + (10 * yDistance));
            }
            else
            {
                return ((14 * xDistance) + (10 * (zDistance - xDistance)) + (10 * yDistance));
            }
        }
    }
}
