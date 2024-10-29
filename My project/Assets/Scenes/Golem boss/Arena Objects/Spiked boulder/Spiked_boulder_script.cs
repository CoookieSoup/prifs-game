using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_boulder_script : MonoBehaviour
{
    public Golem_moveset golem_script;
    public int direction_start;
    public bool is_correct_direction = true;
    void Start()
    {
        //StartCoroutine(DelayedExecution());
        golem_script = FindObjectOfType<Golem_moveset>();
        //spiked_boulder_rigidbody2D = GetComponent<Rigidbody2D>();

    }
    /*
    IEnumerator DelayedExecution()  //Delay for setting the bool is_correct_direction as due to spawning quirks this does not work on start
    {
        yield return new WaitForSeconds(0.1f);
        if (transform.position.x > 0f && transform.position.y > 0f && direction_start == 1)
        {
            is_correct_direction = false;
        }
        if (transform.position.x < 0f && transform.position.y < 0f && direction_start == -1)
        {
            is_correct_direction = false;
        }
        //can_start = true;
    }
    */
    void Update()
    {
        if (direction_start == 1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + golem_script.spiked_boulder_speed * Time.deltaTime);
            if (transform.position.y >= 4.3f)
            {
                direction_start = 0;
            }
        }
        if (direction_start == -1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - golem_script.spiked_boulder_speed * Time.deltaTime);
            if (transform.position.y <= -4.2f)
            {
                direction_start = 0;
            }
        }
        if (transform.position.x <= -8.96f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + golem_script.spiked_boulder_speed * Time.deltaTime);
        }
         if (transform.position.x >= 9.23f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - golem_script.spiked_boulder_speed * Time.deltaTime);
        }
         if (transform.position.y >= 4.12f)
        {
            transform.position = new Vector2(transform.position.x + golem_script.spiked_boulder_speed * Time.deltaTime, transform.position.y);
        }
         if (transform.position.y <= -3.93f)
        {
            transform.position = new Vector2(transform.position.x - golem_script.spiked_boulder_speed * Time.deltaTime  , transform.position.y);
        }
        //Logic to change direction clockwise after colliding with a wall
        /*
        if (Mathf.Abs(spiked_boulder_rigidbody2D.velocity.x) <= 0.01f && can_start && Mathf.Abs(spiked_boulder_rigidbody2D.velocity.y) <= 0.01f)
        {
            if (transform.position.x > 0f && transform.position.y > 0f && is_correct_direction)
            {
                spiked_boulder_rigidbody2D.velocity = new Vector2(0f, -golem_script.spiked_boulder_speed);
            }
            if (transform.position.x > 0f && transform.position.y > 0f && !is_correct_direction)    //case when boss is on the right side of the screen, this is needed because otherwise the projectile would travel down on contact
            {
                spiked_boulder_rigidbody2D.velocity = new Vector2(golem_script.spiked_boulder_speed, 0f);
                is_correct_direction = true;
            }
            if (transform.position.x < 0f && transform.position.y > 0f)
            {
                spiked_boulder_rigidbody2D.velocity = new Vector2(golem_script.spiked_boulder_speed, 0f);
            }
            if (transform.position.x > 0f && transform.position.y < 0f)
            {
                spiked_boulder_rigidbody2D.velocity = new Vector2(-golem_script.spiked_boulder_speed, 0f);
            }
            if (transform.position.x < 0f && transform.position.y < 0f && is_correct_direction)
            {
                spiked_boulder_rigidbody2D.velocity = new Vector2(0f, golem_script.spiked_boulder_speed);
            }
            if (transform.position.x < 0f && transform.position.y < 0f && !is_correct_direction)    //same as ^ comment but upwards
            {
                spiked_boulder_rigidbody2D.velocity = new Vector2(-golem_script.spiked_boulder_speed, 0f);
                is_correct_direction = true;
            }
        }
        */
    }
}
