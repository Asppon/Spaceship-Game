using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //reset MoveDelta
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x,y,0);
        
        //swap sprite direction wether you are going right or left
        if(moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if(moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);

         //Actual movement script   
        transform.Translate(moveDelta * Time.deltaTime);
        }

    }



