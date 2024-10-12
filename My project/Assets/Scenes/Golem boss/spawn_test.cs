using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn_test : MonoBehaviour
{
    public GameObject projectile;
    public Transform proj_spawnpoint;
    private int spawnAngle = 0;
    public float projectile_speed;
    private float timer = 0f;
    public float projectile_spawn_interval;
    public int consequetive_projectile_angle_degrees;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > projectile_spawn_interval)
        {
            timer = 0f;
            var bullet1 = Instantiate(projectile, transform.position, transform.rotation);  //Projectile spawning
            var bullet2 = Instantiate(projectile, transform.position, transform.rotation);
            float radians = spawnAngle * Mathf.Deg2Rad;
            Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)); 
            bullet1.GetComponent<Rigidbody2D>().velocity = movementDirection * projectile_speed;
            bullet2.GetComponent<Rigidbody2D>().velocity = -movementDirection * projectile_speed;

            spawnAngle += consequetive_projectile_angle_degrees; //Next projectile spawn angle (to make circular spawning possible)
        }
    }
}
