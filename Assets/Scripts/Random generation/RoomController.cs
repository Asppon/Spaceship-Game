using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;  //help position rooms
    public int Y; 
}
public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "ShipEntrance";   //helps with sorting scenes

    RoomInfo currentLoadRoomData;

    CurrentRoom thisRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    //adds rooms to queue to be loaded next
    public List<CurrentRoom> loadedRooms = new List<CurrentRoom>();

    bool isLoadingRoom = false; 
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }

    public void LoadRoom( string name, int x, int y)
    {
        if(DoesRoomExist(x,y) == true) //if there is already a room here, dont create a one here as well
        {
            return; 
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;         //assigns the room with info
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData); //adds new room data to the queue
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name; //adds the ships's level name to the name of the scene

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive); //adds exsisting room scene to this scene
        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    
    } //allows us to access seperate room scenes in the same scene

    public void RegisterRoom(CurrentRoom room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + "," + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.thisRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public bool DoesRoomExist(int x,int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public CurrentRoom FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnPlayerEnterRoom(CurrentRoom room)
    {
        CameraController.instance.thisRoom = room;
        thisRoom = room;
        
    } 

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom) { StartCoroutine(SpawnBossRoom()); }

            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (CurrentRoom room in loadedRooms) { room.RemoveUnconnectedDoors(); }

                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue(); //once room is generated you can dequeue it
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);

        if (loadRoomQueue.Count == 0)
        {
            CurrentRoom bossRoom = loadedRooms[loadedRooms.Count - 1];
            CurrentRoom tempRoom = new CurrentRoom(bossRoom.X, bossRoom.Y);

            // Remove the existing boss room and load a new one
            Destroy(bossRoom.gameObject); //cause room is blank
            loadedRooms.Remove(loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y));
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    void Update()
    {
        UpdateRoomQueue();
    }
}

