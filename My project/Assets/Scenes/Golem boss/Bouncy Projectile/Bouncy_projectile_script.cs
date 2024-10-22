using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy_projectile_script : MonoBehaviour
{
    private int bounce_amount;
    public Rigidbody2D bouncy_projectile_rigidBody2D;
    public Transform bouncy_projectile_transform;
    public Golem_moveset golem_script;
    void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
    }
    private void Update()
    {
        Vector2 rotationDirection = bouncy_projectile_rigidBody2D.velocity;
        float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
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
        {
            collision.rigidbody.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        
    }
}
