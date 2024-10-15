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
        if (transform.position.y <= 0f && !is_horizontal)
        {
            log_rigidbody2D.velocity = Vector2.zero;
            log_rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
        if (golem_script.do_despawn_logs || transform.position.x >= 30f || transform.position.x <= -30f)
        {
            Destroy(gameObject);
        }

        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * golem_script.log_speed, transform.localScale.z);
    }
}
