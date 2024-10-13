using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy_projectile_script : MonoBehaviour
{
    private int bounce_amount = 0;
    public Rigidbody2D bouncy_projectile_rigidBody2D;
    public Transform bouncy_projectile_transform;
    public Golem_moveset golem_script;
    void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            bounce_amount++;
            if (bounce_amount > golem_script.bouncy_projectile_max_bounces)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
