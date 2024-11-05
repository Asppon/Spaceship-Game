using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private CurrentRoom room; //serialized field for easier editing
    [SerializeField] private GameObject gridTile;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    [SerializeField] private Grid grid; //allows adjustment in the editor

    private List<Vector2> availablePoints = new List<Vector2>();

    void Awake()
    {
        room = GetComponentInParent<CurrentRoom>();

        //calculate grid dimensions 
        CalculateGridDimensions();
    }

    //calculate grid dimensions based on room width and height
    private void CalculateGridDimensions()
    {
        grid.columns = room.Width - 2;
        grid.rows = room.Height - 2;

        //generate the grid once dimensions are calculated
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        //adjust offsets for clarity
        float verticalTileOffset = room.transform.localPosition.y + grid.verticalOffset;
        float horizontalTileOffset = room.transform.localPosition.x + grid.horizontalOffset;

        //generate grid tiles
        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                //calculate x and y position directly
                float xPos = x + horizontalTileOffset;
                float yPos = y + verticalTileOffset;

                //instantiate grid tile
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector2(xPos, yPos);
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }
    }
}
