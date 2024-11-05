using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_controller : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet = false;

    private Vector2 lastPosition;

    private Vector2 curPosition;

    private Vector2 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay()); //a coroutine allows you to wait for a routine
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //calls the predefined tag from unity
        GameObject wall = GameObject.FindGameObjectWithTag("wall");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    void FixedUpdate()
    {
        if(isEnemyBullet)
        {
            curPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 5f * Time.deltaTime); //makes bullet work for enemy and player
            if(curPosition == lastPosition)
            {
                Destroy(gameObject);
            }
            lastPosition = curPosition;
        }
        
        GameObject selfCol = GameObject.FindGameObjectWithTag("bullet");  //ignores self collisions with player if tag is own bullet
        Physics2D.IgnoreCollision(selfCol.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    // Update is called once per frame

    public void GetPlayer(Transform player)
    {
        playerPosition= player.position;
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject); //destroy built-in to unity
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         if(col.tag == "wall") //destroys bullet if it touches a wall
        {
            Destroy(gameObject);
        }
    
        if(col.tag == "Enemy" && !isEnemyBullet)
        {
            col.gameObject.GetComponent<EnemyStateController>().Death();
            Destroy(gameObject); //kills enemy if it touches it
        }

        if(col.tag == "Player" && isEnemyBullet)
        {
            GameController.DamagePlayer(1);
            Destroy(gameObject); //if bullet touches player and its an enemy bullet then it will damage player and destory bullet
        }
    }

}



