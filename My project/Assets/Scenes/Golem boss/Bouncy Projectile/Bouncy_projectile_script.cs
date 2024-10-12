using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy_projectile_script : MonoBehaviour
{
    private int bounce_amount = 0;
    private int max_bounces = 2;
    public Rigidbody2D bouncy_projectile_rigidBody2D;
    public Transform bouncy_projectile_transform;
    void Start()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            bounce_amount++;
            if (bounce_amount > max_bounces)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
    void Update()
    {

    }
}
