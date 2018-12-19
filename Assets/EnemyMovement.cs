using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // Path is a list of blocks in the world
    [SerializeField] List<WayPoint> path;

	// Use this for initialization
	void Start () {  
        // StartCoroutine(FollowPath());
        // print("Starting patrol");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FollowPath () {
        if (path != null) {
            foreach (WayPoint wayPoint in path) {
                transform.position = wayPoint.transform.position;
                print("Visiting " + wayPoint);
                yield return new WaitForSeconds(1f);
            }
        }
        print("Ending patrol");
    }
}
