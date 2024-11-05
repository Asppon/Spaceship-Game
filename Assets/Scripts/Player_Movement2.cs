using UnityEngine;

public class Player_Movement2 : MonoBehaviour
{
    public static int collectedAmount = 0; //sets items collected to 0
    public float moveSpeed = 5f; //set movement speed

    public Rigidbody2D rb;
    public Animator animator;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFireTime;
    public float fireRate;
    public float damage;

    Vector2 movement;

    void Update()
    {
        //retrieve game controller parameters
        fireRate = GameController.FireRate;
        moveSpeed = GameController.MoveSpeed;
        damage = GameController.Damage;

        //handle player movement
        HandleMovementInput();

        //handle shooting
        HandleShooting();
    }

    void HandleMovementInput()
    {
        //get input for movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //update animator parameters
        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);
        animator.SetFloat("Speed", new Vector2(horizontalInput, verticalInput).sqrMagnitude);

        //move the player
        movement = new Vector2(horizontalInput, verticalInput);
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleShooting()
    {
        //get input for shooting
        float shootHorizontal = Input.GetAxis("shoot_horizontal");
        float shootVertical = Input.GetAxis("shoot_vertical");

        //check if player is shooting and enough time has passed since last shot
        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFireTime + fireRate)
        {
            //shoot bullets
            Shoot(shootHorizontal, shootVertical);
            lastFireTime = Time.time; //update last fire time
        }
    }

    void Shoot(float x, float y)
    {
        //instantiate bullet and set its properties
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.tag = "bullet"; //applies bullet tag so it can be deleted when an enemy touches it
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0); //vector 3 to control bullet speed when shooting
    }
}

