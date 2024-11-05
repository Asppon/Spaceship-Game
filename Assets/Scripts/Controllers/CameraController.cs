using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;// instance of the camera controller
    public CurrentRoom thisRoom;// reference to the current room
    public float moveSpeedWhenRoomChange;// speed at which the camera moves when the room changes
    private Vector3 targetPosition;// target position for the camera to move towards

    // method called when the object is initialized
    void Awake()
    {
        // set the instance to this object
        instance = this;
    }

    // method called every frame
    void Update()
    {
        // check if the current room is not null
        if (thisRoom != null)
        {
            // update the position of the camera
            UpdatePosition();
        }
    }

    // method to update the position of the camera
    void UpdatePosition()
    {

        // check if the target position is not equal to the camera's target position
        if (targetPosition != GetCameraTargetPosition())
        {
            // set the target position to the camera's target position
            targetPosition = GetCameraTargetPosition();
        }

        // move the camera towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeedWhenRoomChange * Time.deltaTime);
    }

    // method to get the target position for the camera
    Vector3 GetCameraTargetPosition()
    {
        // check if the current room is null
        if (thisRoom == null)
        {
            // return zero vector if the room is null
            return Vector3.zero;
        }

        // get the center position of the room
        Vector3 roomCenter = thisRoom.GetRoomCentre();
        // maintain the camera's Z position and return the target position
        return new Vector3(roomCenter.x, roomCenter.y, transform.position.z);
    }

    // method to check if the camera is switching scenes
    public bool IsSwitchingScene()
    {
        // return true if the camera's position is not equal to the target position
        return transform.position != targetPosition;
    }
}
