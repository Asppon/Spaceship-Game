using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom 
    }
    //lets me set each door to a specific location
    public DoorType doorType;
}

