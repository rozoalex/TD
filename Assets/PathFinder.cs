using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    Dictionary<Vector2, WayPoint> grid = new Dictionary<Vector2, WayPoint>();

    Vector2[] directions = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

    List<WayPoint> shortestPath = new List<WayPoint>();

    [SerializeField] WayPoint start, end;

    // Use this for initialization
    void Start () {
        LoadBlocks();
        ColorStartAndEnd();
        StartCoroutine(ExploreNeighbours());
	}

    IEnumerator ExploreNeighbours()
    {
        Path path = FindPath(start);
        while (path != null) {
            shortestPath.Insert(0, path.current);
            grid[path.current.GetGridPos()].SetTopColor(Color.white);
            path = path.prev;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private Path FindPath(WayPoint start)
    {
        // Initialize BFS
        Stack<Path> paths = new Stack<Path>();
        HashSet<Vector2> visited = new HashSet<Vector2>();
        paths.Push(new Path(null, start));
        // Repeat while paths not empty
        while (paths.Count > 0) {
            Stack<Path> nextPaths = new Stack<Path>();
            while (paths.Count > 0) {
                Path currentPath = paths.Pop();
                print(currentPath.current);
                if (currentPath.current == end) { return currentPath; } 
                Vector2 currentPos = currentPath.current.GetGridPos();
                foreach (Vector2 direction in directions) {
                    Vector2 next = currentPos + direction;
                    if (grid.ContainsKey(next) && !visited.Contains(next)) {
                        nextPaths.Push(new Path(currentPath, grid[next]));
                    }
                }
                visited.Add(currentPos);
            }
            paths = nextPaths;
        }
        return null;
    }

    private class Path {
        public Path prev;
        public WayPoint current;
        public Path(Path prev, WayPoint current) {
            this.prev = prev;
            this.current = current;
        }
    }

    private void ColorStartAndEnd()
    {
        start.SetTopColor(Color.white);
        end.SetTopColor(Color.black);
    }

    private void LoadBlocks()
    {
        WayPoint[] wayPoints = FindObjectsOfType<WayPoint>();
        foreach (WayPoint point in wayPoints) {
            if (grid.ContainsKey(point.GetGridPos())) {
                Debug.LogWarning("Overlap at: " + point);
            } else {
                grid.Add(point.GetGridPos(), point);
            }
            
        }
        print("Loaded " + grid.Count + " blocks");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
