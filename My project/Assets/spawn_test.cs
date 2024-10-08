using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn_test : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform proj_spawnpoint;
    private int spawnAngle = 0;
    public float projectile_speed;
    private float timer = 0f;
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            timer = 0f;
            var bullet1 = Instantiate(projectile, transform.position, transform.rotation);  //Projectile spawning
            var bullet2 = Instantiate(projectile, transform.position, transform.rotation);
            float radians = spawnAngle * Mathf.Deg2Rad;
            Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            bullet1.GetComponent<Rigidbody2D>().velocity = movementDirection * projectile_speed;
            bullet2.GetComponent<Rigidbody2D>().velocity = -movementDirection * projectile_speed;

            spawnAngle += 7;
        }
    }
}
