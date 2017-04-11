using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GridMaster
{
    public class GridBase : MonoBehaviour
    {
        // Grid Settings
        [SerializeField]
        int xMax = 10;
        [SerializeField]
        int yMax = 1;
        [SerializeField]
        int zMax = 10;

        // Not really necessary in this instance
        [SerializeField]
        float xOffset = 1;
        [SerializeField]
        float yOffset = 1;
        [SerializeField]
        float zOffset = 1;

        [SerializeField]
        GameObject gridFloorPrefab;

        [SerializeField]
        Vector3 startNodePosition;
        [SerializeField]
        Vector3 endNodePosition;

        public Node[,,] grid;

        // Singleton Pattern
        static GridBase instance;
        public static GridBase Instance
        {
            get
            {
                return instance;
            }
        }

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            grid = new Node[xMax, yMax, zMax];

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    for (int z = 0; z < zMax; z++)
                    {
                        float xPosition = x * xOffset;
                        float yPosition = y * yOffset;
                        float zPosition = z * zOffset;

                        GameObject go = Instantiate(gridFloorPrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity) as GameObject;
                        go.transform.name = x.ToString() + "" + y.ToString() + "" + z.ToString();
                        go.transform.parent = this.transform;

                        Node node = new Node();
                        node.xPosition = x;
                        node.yPosition = y;
                        node.zPosition = z;
                        node.worldObject = go;

                        grid[x, y, z] = node;
                    }
                }
            }
        }

        // For testing
        [SerializeField]
        bool startTest;

        void Update()
        {
            if (startTest)
            {
                startTest = false;

                Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                // Make a node unwalkable to test avoidance
                grid[1, 0, 1].isWalkable = false;

                // Get the target nodes
                Node startNode = GetNodeFromVector3(startNodePosition);
                Node endNode = GetNodeFromVector3(endNodePosition);

                path.startPosition = startNode;
                path.endPosition = endNode;

                // Find the path
                List<Node> p = path.FindPath();

                // Disable the world object for each node passed through
                startNode.worldObject.SetActive(false);
                foreach (Node n in p)
                {
                    n.worldObject.SetActive(false);
                }
            }
        }

        public Node GetNodeFromVector3(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            int z = Mathf.RoundToInt(position.z);

            Node returnNode = GetNode(x, y, z);

            return returnNode;
        }

        public Node GetNode(int x, int y, int z)
        {
            // will return null if greater than max values

            Node returnNode = null;

            if (x < xMax && x >= 0 && y < yMax && y >= 0 && z < zMax && z >= 0)
            {
                returnNode = grid[x, y, z];
            }

            return returnNode;
        }
    }
}
