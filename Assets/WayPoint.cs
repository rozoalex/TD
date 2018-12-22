using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint: MonoBehaviour {

    Vector2 gridPos;

    const int gridSize = 10;

    public WayPoint prev;

    public int GetGridSize() { return gridSize; }

    public Vector2 GetGridPos() {
        return new Vector2(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }

    public void SetTopColor(Color color) {
        MeshRenderer top = transform.Find("top").GetComponent<MeshRenderer>();
        top.material.color = color;
    }
}
