using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_projectile_script : MonoBehaviour
{
    private int bounce_amount = 0;
    public Rigidbody2D bomb_projectile_rigidBody2D;
    public Transform bomb_projectile_transform;
    public float explode_cooldown;
    public Golem_moveset golem_script;
    public GameObject bouncy_projectile;
    void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            bounce_amount++;
            if (bounce_amount > golem_script.bomb_max_bounces)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
    void Update()
    {
        explode_cooldown -= Time.deltaTime;
        if (explode_cooldown < 0)
        {
            int spawnAngle = 0;
            
            if (!golem_script.isPhase2)
                for (int i = 0; i < 8; i++) //Less projectiles for phase 1
                {
                    float random_offset = Random.Range(-20f, 20f);
                    float radians = (spawnAngle + random_offset) * Mathf.Deg2Rad;
                    Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
                    var BouncyProjectile = Instantiate(bouncy_projectile, transform.position, transform.rotation);
                    BouncyProjectile.GetComponent<Rigidbody2D>().velocity = movementDirection * golem_script.bomb_projectile_speed;

                    Vector2 rotationDirection = BouncyProjectile.GetComponent<Rigidbody2D>().velocity;   
                    float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

                    spawnAngle += 45;
                }
            else
            {
                for (int i = 0; i < 10; i++) //More projectiles for phase 2
                {
                    float random_offset = Random.Range(-20f, 20f);
                    float radians = (spawnAngle + random_offset) * Mathf.Deg2Rad;
                    Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
                    var BouncyProjectile = Instantiate(bouncy_projectile, transform.position, transform.rotation);
                    BouncyProjectile.GetComponent<Rigidbody2D>().velocity = movementDirection * golem_script.bomb_projectile_speed;

                    Vector2 rotationDirection = BouncyProjectile.GetComponent<Rigidbody2D>().velocity;
                    float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

                    spawnAngle += 36;
                }
            }
            Destroy(gameObject);
        }
    }
}
