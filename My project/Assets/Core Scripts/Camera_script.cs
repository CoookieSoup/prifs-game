using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public Transform player_transform;
    private Rigidbody2D camera_rigidBody2D;
    public float camera_speed;
    void Start()
    {
        if (player_transform.position.x <= -7.8f) transform.position = new Vector3(-7.8f, 0f, -10f);
        else transform.position = new Vector3(player_transform.position.x, 0f, -10f);
        camera_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player_transform.position.x >= 6.9f)
        {
            Vector2 direction = new Vector2(6.9f - transform.position.x, 0f); // Didn't normalise to make cam movement feel less snappy
            camera_rigidBody2D.velocity = direction * camera_speed;
        }
        else if (player_transform.position.x <= -7.8f)
        {
            Vector2 direction = new Vector2(-7.8f - transform.position.x, 0f); 
            camera_rigidBody2D.velocity = direction * camera_speed;
        }
        else if (Mathf.Abs(player_transform.position.x - transform.position.x) > 0.01f)
        {
            Vector2 direction = new Vector2(player_transform.position.x - transform.position.x, 0f); 
            camera_rigidBody2D.velocity = direction * camera_speed;
        }
        else
        {
            camera_rigidBody2D.velocity = Vector2.zero;
        }
    }
}
