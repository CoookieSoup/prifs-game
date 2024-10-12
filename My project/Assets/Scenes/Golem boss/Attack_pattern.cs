using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_pattern : MonoBehaviour
{
    public GameObject projectile;
    public Transform player_transform;
    private int spawnAngle = 0;
    public float projectile_speed;
    private float timer = 0f;
    public float projectile_spawn_interval;
    public int consequetive_projectile_angle_degrees;
    private int boss_health;
    private bool isPhase2 = false;

    //testing
    public bool isTest1 = false;
    public bool isTest2 = false;
    void Start()
    {
        boss_health = 1000;
        isTest2 = true;
    }


    void Update()
    {

        timer += Time.deltaTime;
        if (isTest1)
            CircularAttack1();
        if (isTest1)
            BounceAttack1();
    }


    void CircularAttack1()
    {
        if (timer > projectile_spawn_interval)
        {
            timer = 0f;
            var bullet1 = Instantiate(projectile, transform.position, transform.rotation);  //Projectile spawning
            var bullet2 = Instantiate(projectile, transform.position, transform.rotation);
            float radians = spawnAngle * Mathf.Deg2Rad;
            Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            bullet1.GetComponent<Rigidbody2D>().velocity = movementDirection * projectile_speed;
            bullet2.GetComponent<Rigidbody2D>().velocity = -movementDirection * projectile_speed;
            if (isPhase2) //For the creation of a 3rd and 4th circle of projectiles
            {
                float radiansExtra = (spawnAngle + 90) * Mathf.Deg2Rad; //Essentially same as bullet1 and bullet2 but shifter 90 degrees
                Vector2 movementDirection1 = new Vector2(Mathf.Cos(radiansExtra), Mathf.Sin(radiansExtra));
                var bullet3 = Instantiate(projectile, transform.position, transform.rotation);
                var bullet4 = Instantiate(projectile, transform.position, transform.rotation);
                bullet3.GetComponent<Rigidbody2D>().velocity = movementDirection1 * projectile_speed;
                bullet4.GetComponent<Rigidbody2D>().velocity = -movementDirection1 * projectile_speed;
            }
            spawnAngle += consequetive_projectile_angle_degrees; //Next projectile spawn angle (to make circular spawning possible)
        }
    }
    void BounceAttack1()
    {
        if (timer > projectile_spawn_interval)
        {
            timer = 0f;
            var bullet1 = Instantiate(projectile, transform.position, transform.rotation);  //Projectile spawning
            var bullet2 = Instantiate(projectile, transform.position, transform.rotation);
            float radians = spawnAngle * Mathf.Deg2Rad;
            Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            bullet1.GetComponent<Rigidbody2D>().velocity = movementDirection * projectile_speed;
            bullet2.GetComponent<Rigidbody2D>().velocity = -movementDirection * projectile_speed;
            if (isPhase2) //For the creation of a 3rd and 4th circle of projectiles
            {
                float radiansExtra = (spawnAngle + 90) * Mathf.Deg2Rad; //Essentially same as bullet1 and bullet2 but shifter 90 degrees
                Vector2 movementDirection1 = new Vector2(Mathf.Cos(radiansExtra), Mathf.Sin(radiansExtra));
                var bullet3 = Instantiate(projectile, transform.position, transform.rotation);
                var bullet4 = Instantiate(projectile, transform.position, transform.rotation);
                bullet3.GetComponent<Rigidbody2D>().velocity = movementDirection1 * projectile_speed;
                bullet4.GetComponent<Rigidbody2D>().velocity = -movementDirection1 * projectile_speed;
            }
        }
    }
}
