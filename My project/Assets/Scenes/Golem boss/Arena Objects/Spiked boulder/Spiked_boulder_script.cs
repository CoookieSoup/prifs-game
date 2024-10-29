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
        golem_script = FindObjectOfType<Golem_moveset>();
    }
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
    }
}
