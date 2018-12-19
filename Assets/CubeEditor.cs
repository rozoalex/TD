// The EditorSnap helps the cube to snap to the grid
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    WayPoint wayPoint;

    private void Awake()
    {
        wayPoint = GetComponent<WayPoint>();
    }


    void Update()
    {
        SnapToGrid();
        UpdateLabel();

    }

    private void SnapToGrid()
    {
        Vector2 gridPos = wayPoint.GetGridPos();
        int gridSisze = wayPoint.GetGridSize();
        transform.position = new Vector3(gridPos.x * gridSisze, 0f, gridPos.y * gridSisze);
    }

    private void UpdateLabel()
    {
        int gridSize = wayPoint.GetGridSize();
        Vector2 gridPos = wayPoint.GetGridPos();
        string labelText = gridPos.x + "," + gridPos.y;
        // Set up the lab with TextMesh
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = labelText;
        gameObject.name = "cube (" + labelText + ")";
    }
}