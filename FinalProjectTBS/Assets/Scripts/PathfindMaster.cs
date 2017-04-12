using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GridMaster;
using System.Threading;

namespace Pathfinding
{
    public class PathfindMaster : MonoBehaviour
    {
        [SerializeField]
        int maxJobs = 3;

        public delegate void PathfindingJobComplete(List<Node> path);

        List<Pathfinder> currentJobs;
        List<Pathfinder> todoJobs;

        // Singleton Pattern
        static PathfindMaster instance;
        public static PathfindMaster Instance
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
            currentJobs = new List<Pathfinder>();
            todoJobs = new List<Pathfinder>();
        }
        
        void Update()
        {
            int i = 0;
            while (i < currentJobs.Count)
            {
                if (currentJobs[i].jobDone)
                {
                    currentJobs[i].NotifyComplete();
                    currentJobs.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            if (todoJobs.Count > 0 && currentJobs.Count < maxJobs)
            {
                Pathfinder job = todoJobs[0];
                todoJobs.RemoveAt(0);
                currentJobs.Add(job);

                // Start a new thread for job
                Thread jobThread = new Thread(job.FindPath);
                jobThread.Start();
            }
        }

        public void RequestPathfind(Node start, Node target, PathfindingJobComplete completeCallback)
        {
            Pathfinder newJob = new Pathfinder(start, target, completeCallback);
            todoJobs.Add(newJob);
        }
    }
}
