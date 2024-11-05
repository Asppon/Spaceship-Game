using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // instance of the game controller
    public static GameController instance;

    // variables for player stats
    private static float health = 5f;
    private static float moveSpeed = 5f;
    private static float fireRate = 1.5f;
    private static float damage = 1;
    private static int playerHealth = 5;

    // properties to access player stats
    public static float Health
    {
        get { return health; }
        set { health = value; }
    }

    public static float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public static float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    public static int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public static float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // UI text element for displaying player health
    public Text healthText;

    // method called when the object is initialized
    private void Awake()
    {
        // set the instance to this object
        if (instance == null)
        {
            instance = this; //if stats are unknown call this main script
        }
    }

    // method to damage the player
    public static void DamagePlayer(int damage)
    {
        // reduce player health by the damage amount
        health = health - damage;

        // check if player health is less than or equal to zero
        if (Health <= 0)
        {
            // kill the player if health is zero or below
            KillPlayer();
        }

    }

    // method to heal the player
    public static void HealPlayer(float healAmount)
    { health = Mathf.Min(playerHealth, health + healAmount);
    }
    public static void MoveSpeedChange(float speed) 
    {
        moveSpeed += speed;
    }
    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }
    public static void DamageChange(float damageInc)
    {
        damage = damageInc;
    }


    // method to kill the player
    private static void KillPlayer()
    {
        // quit the application
        Application.Quit();
    }

    // method to heal the player with integer value
    public static void HealPlayer(int healAmount)
    {
        // set player health to the minimum of the maximum health or current health plus heal amount
        Health = Mathf.Min(playerHealth + healAmount);
    }

}


