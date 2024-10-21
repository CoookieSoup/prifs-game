using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked_boulder_script : MonoBehaviour
{
    public Rigidbody2D spiked_boulder_rigidbody2D;
    public Golem_moveset golem_script;
    public int direction_start;
    public bool is_correct_direction = true;
    void Start()
    {
        StartCoroutine(DelayedExecution());
        golem_script = FindObjectOfType<Golem_moveset>();
        spiked_boulder_rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
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
    }
    
    
    void Update()
    {
        //Logic to change direction clockwise after colliding with a wall
        if (spiked_boulder_rigidbody2D.velocity == Vector2.zero)
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
    }
}
