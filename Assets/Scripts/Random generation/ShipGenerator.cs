using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour
{
    public ShipGenerationData shipGenerationData;
    private List<Vector2Int> shipRooms;

    private void Start()
    {
        //generate ship rooms based on ship generation data
        GenerateShipRooms();

        //spawn generated rooms
        SpawnRooms(shipRooms);
    }

    //generate ship rooms using ShipCrawlerController
    private void GenerateShipRooms()
    {
        shipRooms = ShipCrawlerController.GenerateShip(shipGenerationData);
    }

    //spawn rooms based on the provided room locations
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        //load the starting room
        RoomController.instance.LoadRoom("Start", 0, 0);

        //load each room at its respective location
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
        }
    }
}


