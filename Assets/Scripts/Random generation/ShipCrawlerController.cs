using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};

public class ShipCrawlerController : MonoBehaviour
{
    // list to store visited positions
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    // dictionary mapping directions to corresponding movement vectors
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        // initialize the dictionary with direction and movement vector pairs
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };

    // method to generate ship crawling behavior
    public static List<Vector2Int> GenerateShip(ShipGenerationData shipData)
    {
        // list to store visited positions during generation
        List<Vector2Int> positionsVisited = new List<Vector2Int>();

        // list to store ship crawlers
        List<ShipCrawler> shipCrawlers = new List<ShipCrawler>();

        // loop to create ship crawlers
        for (int i = 0; i < shipData.numberOfCrawlers; i++)
        {
            shipCrawlers.Add(new ShipCrawler(Vector2Int.zero));
        }

        // calculate the number of iterations for ship crawling
        int iterations = Random.Range(shipData.iterationMin, shipData.iterationMax);

        // loop through iterations to simulate ship crawling
        for (int i = 0; i < iterations; i++)
        {
            // loop through ship crawlers to move each one
            foreach (ShipCrawler shipCrawler in shipCrawlers)
            {
                // move the ship crawler in a specific direction and add the new position to the list
                Vector2Int newPos = shipCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }
        // return the list of visited positions
        return positionsVisited;
    }
}

