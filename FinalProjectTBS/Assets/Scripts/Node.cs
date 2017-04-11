using UnityEngine;
using System.Collections;

namespace GridMaster
{
    public class Node
    {
        // Node Grid Position
        public int xPosition;
        public int yPosition;
        public int zPosition;

        // Pathfinding Costs
        float hCost;
        public float HCost
        {
            get
            {
                return hCost;
            }
            set
            {
                hCost = value;
            }
        }

        float gCost;
        public float GCost
        {
            get
            {
                return gCost;
            }
            set
            {
                gCost = value;
            }
        }

        public float FCost
        {
            get
            {
                return HCost + gCost;
            }
        }

        public GameObject worldObject;
        public Node parentNode;
        public bool isWalkable;
    }
}
