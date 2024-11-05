using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shooting : MonoBehaviour
{
    // prefab for bullets
    public GameObject bulletPrefab;
    // speed of bullets
    public float bulletSpeed;
    // time since last firing
    private float lastFire;
    // delay between consecutive fires
    public float fireDelay;


    // Update is called once per frame
    void Update()
    {
        // get input for shooting direction
        float shootHor = Input.GetAxis("shoot horizontal");
        float shootVert = Input.GetAxis("shoot vertical");

    }
}

