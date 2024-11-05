using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour
{
    // width and height of the room
    public int Width;

    // height of the room
    public int Height;      

    // x-coordinate of the room
    public int X;

    // y-coordinate of the room
    public int Y;

    // bool indicating if doors are initialized
    private bool doorsInitialized = false;

    // constructor for setting x and y coordinates
    public CurrentRoom(int x, int y)
    {
        X = x;
        Y = y;
    }

    // variables representing different doors of the room
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    // list to store all doors in the room
    public List<Door> doors = new List<Door>();

    // method called when the script is started
    void Start()
    {
        // check if instance of room controller exists
        if (RoomController.instance != null)
        {
            // initialize doors and register the room
            InitializeDoors();
            RoomController.instance.RegisterRoom(this);
        }
    }

    void Update()
    {
        // check if the room name contains "End" and doors are not initialized
        if (name.Contains("End") && !doorsInitialized)
        {
            // remove unconnected doors and set bool to true
            RemoveUnconnectedDoors();
            doorsInitialized = true;
        }
    }

    // method to initialize doors of the room
    private void InitializeDoors()
    {
        // get all door components in children
        Door[] doors = GetComponentsInChildren<Door>();

        // dictionary to map door types to doors
        Dictionary<Door.DoorType, Door> doorMap = new Dictionary<Door.DoorType, Door>();

        // loop through all doors
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    doorMap[Door.DoorType.right] = door;
                    break;
                case Door.DoorType.left:
                    doorMap[Door.DoorType.left] = door;
                    break;
                case Door.DoorType.top:
                    doorMap[Door.DoorType.top] = door;
                    break;
                case Door.DoorType.bottom:
                    doorMap[Door.DoorType.bottom] = door;
                    break;
            }
        }
        // assign door variables based on door type
        rightDoor = doorMap.GetValueOrDefault(Door.DoorType.right);
        leftDoor = doorMap.GetValueOrDefault(Door.DoorType.left);
        topDoor = doorMap.GetValueOrDefault(Door.DoorType.top);
        bottomDoor = doorMap.GetValueOrDefault(Door.DoorType.bottom);
    }

    // bool indicating if the room is a boss room
    public bool IsBossRoom = false;

    // method to remove unconnected doors from the room
    public void RemoveUnconnectedDoors()
    {
        // if it's a boss room, return
        if (IsBossRoom) return;

        // deactivate doors if adjacent room is null
        if (GetRight() == null) { rightDoor.gameObject.SetActive(false); }
        if (GetLeft() == null) { leftDoor.gameObject.SetActive(false); }
        if (GetTop() == null) { topDoor.gameObject.SetActive(false); }
        if (GetBottom() == null) { bottomDoor.gameObject.SetActive(false); }
    }

    // method to get adjacent room
    public CurrentRoom GetAdjacentRoom(int xOffset, int yOffset)
    {
        // return adjacent room if it exists, otherwise return null
        return RoomController.instance.DoesRoomExist(X + xOffset, Y + yOffset) ?
            RoomController.instance.FindRoom(X + xOffset, Y + yOffset) :
            null;
    }

    public CurrentRoom GetRight() => GetAdjacentRoom(1, 0);
    public CurrentRoom GetLeft() => GetAdjacentRoom(-1, 0); 
    public CurrentRoom GetTop() => GetAdjacentRoom(0, 1); // methods to get adjacent rooms in different directions
    public CurrentRoom GetBottom() => GetAdjacentRoom(0, -1);

    void OnDrawGizmos() // method to draw gizmos for helping ensure rooms are the same size 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0)); // helps me create other rooms so that they remain the same size
    }
   
    public Vector3 GetRoomCentre() // method to locate room centre
    {
        return new Vector3(X * Width, Y * Height);
    }

    // method called when collider enters another collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") { RoomController.instance.OnPlayerEnterRoom(this); } // if collider has tag "Player", notify room controller
    }
}
