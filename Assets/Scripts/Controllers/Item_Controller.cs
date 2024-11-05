using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;         // name of the item
    public string flavourtext;  // flavour text describing the item
    public Sprite itemImage;    // image representing the item
}

public class Item_Controller : MonoBehaviour
{
    public Item item;           // the item associated with this controller

    // changes to player attributes upon picking up the item
    public float healthChange;      // change in player's health
    public float moveSpeedChange;   // change in player's movement speed
    public float fireRateChange;    // change in player's firing rate
    public float damageChange;      // change in player's damage output

    // start is called before the first frame update
    public void Start()
    {
        // remove the existing collider and add a new trigger collider
        Destroy(GetComponent<PolygonCollider2D>());
        var collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }

    // triggered when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the collider belongs to the player
        if (collision.tag == "Player")
        {
            // increment the collected amount and apply attribute changes to the player
            Player_Movement2.collectedAmount++;
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.HealPlayer(healthChange);
            GameController.FireRateChange(fireRateChange);
            GameController.DamageChange(damageChange);
            Destroy(gameObject);  // destroy the item object
        }
    }
}
