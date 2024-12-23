using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log_script : MonoBehaviour
{
    public Golem_moveset golem_script;
    private Rigidbody2D log_rigidbody2D;
    public bool is_horizontal = false;

    void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
        log_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (log_rigidbody2D.velocity != Vector2.zero)
        {
            //Sets the rotation equal to travel direction
            Vector2 rotationDirection = log_rigidbody2D.velocity;
            float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        }
        if (transform.position.y <= 0f && !is_horizontal && log_rigidbody2D.bodyType != RigidbodyType2D.Static)   //For the vertical logs to stay on screen and become immovable
        {
            log_rigidbody2D.velocity = Vector2.zero;
            log_rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
        if (golem_script.do_despawn_logs || transform.position.x >= 30f || transform.position.x <= -30f)    //Unload after travelling ofscreen
        {
            Destroy(gameObject);
        }
    }
}
