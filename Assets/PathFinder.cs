using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    Dictionary<Vector2, WayPoint> grid = new Dictionary<Vector2, WayPoint>();

    Vector2[] directions = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

    [SerializeField] WayPoint start, end;

    // Use this for initialization
    void Start () {
        LoadBlocks();
        ColorStartAndEnd();
	}

    private List<WayPoint> ExploreNeighbours()
    {
        List<WayPoint> shortestPath = new List<WayPoint>();
        WayPoint path = FindShortestPath(start);
        while (path != null) {
            shortestPath.Insert(0, path);
            path = path.prev;
        }
        return shortestPath;
    }

    private WayPoint FindShortestPath(WayPoint start)
    {
        // Initialize BFS
        Stack<WayPoint> paths = new Stack<WayPoint>();
        HashSet<Vector2> visited = new HashSet<Vector2>();
        paths.Push(start);
        // Repeat while paths not empty
        while (paths.Count > 0) {
            Stack<WayPoint> nextPaths = new Stack<WayPoint>();
            while (paths.Count > 0) {
                WayPoint currentPath = paths.Pop();
                print(currentPath.GetGridPos());
                if (currentPath == end) { return currentPath; } 
                Vector2 currentPos = currentPath.GetGridPos();
                foreach (Vector2 direction in directions) {
                    Vector2 next = currentPos + direction;
                    if (grid.ContainsKey(next) && !visited.Contains(next)) {
                        grid[next].prev = currentPath;
                        nextPaths.Push(grid[next]);
                    }
                }
                visited.Add(currentPos);
            }
            paths = nextPaths;
        }
        return null;
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

    public List<WayPoint> GetShortestPath() {
        LoadBlocks();
        return ExploreNeighbours();
    }
}
