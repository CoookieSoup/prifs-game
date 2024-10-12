using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_pattern : MonoBehaviour
{
    
    
    public Transform player_transform;
    private float timer = 0f;
    public bool isPhase2;

    //Projectiles
    public GameObject projectile;
    public GameObject Bouncy_projectile;
    private int spawnAngle = 0;
    public float projectile_speed;
    public float bouncy_projectile_speed;
    public float bouncy_projectile_spawn_interval;
    public float projectile_spawn_interval;
    public int consequetive_projectile_angle_degrees;
    private float projectile_spawn_timer = 0;
    private float bouncy_projectile_spawn_timer = 0;

    //Attack Booleans
    //private bool isBounceAttack1 = false;
    //private bool isCircularAttack1 = false;

    //testing
    public bool isTest1;
    public bool isTest2;
    void Start()
    {
    }


    void Update()
    {

        timer += Time.deltaTime;
        if (isTest1)
            CircularAttack1();
        if (isTest2)
            BounceAttack1();
    }


    void CircularAttack1()
    {
        
        projectile_spawn_timer += Time.deltaTime;
        if (projectile_spawn_timer > projectile_spawn_interval)
        {
            projectile_spawn_timer = 0f;
            var Projectile1 = Instantiate(projectile, transform.position, transform.rotation);  //Projectile spawning
            var Projectile2 = Instantiate(projectile, transform.position, transform.rotation);
            float radians = spawnAngle * Mathf.Deg2Rad;
            Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            Projectile1.GetComponent<Rigidbody2D>().velocity = movementDirection * projectile_speed;
            Projectile2.GetComponent<Rigidbody2D>().velocity = -movementDirection * projectile_speed;
            if (isPhase2) //For the creation of a 3rd and 4th circle of projectiles
            {
                float radiansExtra = (spawnAngle + 90) * Mathf.Deg2Rad; //Essentially same as bullet1 and bullet2 but shifter 90 degrees
                Vector2 movementDirection1 = new Vector2(Mathf.Cos(radiansExtra), Mathf.Sin(radiansExtra));
                var Projectile3 = Instantiate(projectile, transform.position, transform.rotation);
                var Projectile4 = Instantiate(projectile, transform.position, transform.rotation);
                Projectile3.GetComponent<Rigidbody2D>().velocity = movementDirection1 * projectile_speed;
                Projectile4.GetComponent<Rigidbody2D>().velocity = -movementDirection1 * projectile_speed;
            }
            spawnAngle += consequetive_projectile_angle_degrees; //Next projectile spawn angle (to make circular spawning possible)
        }
    }
    void BounceAttack1()
    {
        
        bouncy_projectile_spawn_timer += Time.deltaTime;
        if (bouncy_projectile_spawn_timer > bouncy_projectile_spawn_interval)
        {
            bouncy_projectile_spawn_timer = 0f;
            var BouncyProjectile1 = Instantiate(Bouncy_projectile, transform.position, transform.rotation);  //Projectile spawning
            Vector2 movementDirection = new Vector2(player_transform.position.x - transform.position.x, player_transform.position.y - transform.position.y);
            movementDirection.Normalize();
            BouncyProjectile1.GetComponent<Rigidbody2D>().velocity = movementDirection * bouncy_projectile_speed;
        }
    }
}
