using UnityEngine;
using System.Collections;

public enum EnemyState { Idle, Wander, Follow, Attack, Die }
public enum EnemyType { Melee, Ranged }

public class EnemyStateController : MonoBehaviour
{
    // reference to the player GameObject
    [SerializeField] private GameObject player;
    // initial state of the enemy
    [SerializeField] private EnemyState initialState = EnemyState.Idle;

    // enemy type and associated stats
    [Header("Type and Stats")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float coolDown;
    [SerializeField] private float health;

    // lets me assign the bullet prefab to the ranged enemy
    [Header("Ranged Enemy")]
    [SerializeField] private GameObject bulletPrefab;

    // private variables for state management
    private EnemyState currentState;
    public bool contact = false;
    public bool notInRoom = false;
    private bool dead = false;
    private bool chooseDirection = false;
    private bool coolDownAttack = false;
    private Vector3 randomDirection;

    void Start()
    {
        // ensure player GameObject is assigned, if not find it by tag
        player = player ?? GameObject.FindGameObjectWithTag("Player");
        // set initial state
        currentState = initialState;
    }

    void Update()
    {
        // early exit if dead
        if (dead) return;

        // update enemy state if not out of the room
        if (!notInRoom)
        {
            UpdateEnemyState();
        }

        // execute behaviors based on current state
        switch (currentState)
        {
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Die:
                break; // no behavior needed in the Die state
        }
    }

    // method to update enemy state based on player proximity
    private void UpdateEnemyState()
    {
        if (IsPlayerInRange(range))
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
            {
                currentState = EnemyState.Attack;
            }
            else
            {
                currentState = EnemyState.Follow;
            }
        }
        else
        {
            currentState = EnemyState.Wander;
        }
    }

    // check if player is within a specified range
    private bool IsPlayerInRange(float range)
    {
        return Vector2.Distance(transform.position, player.transform.position) <= range;
    }

    // coroutine to randomly choose direction for wandering
    private IEnumerator ChooseDirection()
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        randomDirection = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDirection = false;
    }

    // method for wandering behavior
    void Wander()
    {
        if (!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }
    }

    // method to handle collisions with other colliders
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            contact = true;
        }
    }

    // method for following the player
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    // method to damage the player on contact
    void PlayerDamage()
    {
        if (contact == true)
        {
            GameController.DamagePlayer(1);
        }
    }

    // method for attacking behavior
    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    bullet.GetComponent<Bullet_controller>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0; //reuse of player projectile code
                    bullet.GetComponent<Bullet_controller>().isEnemyBullet = true; //ensures enemies cant hurt eachother
                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }

    // coroutine for cooldown period between attacks
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        contact = false;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    // method to handle death of the enemy
    public void Death()
    {
        Destroy(gameObject);
    }
}

