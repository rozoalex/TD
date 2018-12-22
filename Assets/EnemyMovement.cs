using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movementTime = 0.5f;

	// Use this for initialization
	void Start () {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<WayPoint> path = pathFinder.GetShortestPath();
        StartCoroutine(FollowPath(path));
        print("Starting patrol");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FollowPath (List<WayPoint> path) {
        if (path != null) {
            foreach (WayPoint wayPoint in path) {
                transform.position = wayPoint.transform.position;
                wayPoint.SetTopColor(Color.white);
                print("Visiting " + wayPoint);
                yield return new WaitForSeconds(movementTime);
            }
        }
        print("Ending patrol");
    }
}
