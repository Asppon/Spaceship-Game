using System.Collections.Generic;
using UnityEngine;

public class ShipCrawler : MonoBehaviour
{
    // private field to store the position of the ship crawler
    private Vector2Int position;

    // property to get and set the position of the ship crawler
    public Vector2Int Position
    {
        get { return position; } // getter for position
        private set { position = value; } // private setter for position
    }

    // constructor to initialize the ship crawler with a starting position
    public ShipCrawler(Vector2Int startPos)
    {
        Position = startPos; // set the initial position
    }

    // method to move the ship crawler based on a direction movement map
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        // create a list of available directions from the keys of the direction movement map
        List<Direction> availableDirections = new List<Direction>(directionMovementMap.Keys);

        // select a random direction to move
        Direction toMove = availableDirections[Random.Range(0, availableDirections.Count)];

        // update the position based on the selected direction
        Position += directionMovementMap[toMove];

        // return the new position
        return Position;
    }
}


