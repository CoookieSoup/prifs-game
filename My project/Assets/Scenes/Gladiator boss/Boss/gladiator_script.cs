using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gladiator_script : MonoBehaviour
{

    public float timer = 0f;

    //Cleave
    public float cleave_telegraph_timer;
    public float cleave_logic_timer = 0f;
    public GameObject cleave_left;
    public GameObject cleave_right;
    public GameObject cleave_warning;

    //Player
    public Transform player_Transform;
    void Start()
    {
        cleave_warning.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            Cleave();
        }
        if (timer >= 10f)
        {
            Cleave_close_package(); //For coding visual clarity
        }
    }
    void Cleave()
    {
        cleave_logic_timer += Time.deltaTime;
        if (cleave_logic_timer < cleave_telegraph_timer)
        {
            cleave_warning.SetActive(true);
        }
        if (cleave_logic_timer > cleave_telegraph_timer)
        {
            cleave_warning.SetActive(false);
            if (player_Transform.position.x > transform.position.x)
            {
                cleave_right.GetComponent<CapsuleCollider2D>().enabled = true;
                cleave_right.GetComponent<SpriteRenderer>().enabled = true;
                cleave_left.GetComponent<CapsuleCollider2D>().enabled = false;
                cleave_left.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                cleave_left.GetComponent<CapsuleCollider2D>().enabled = true;
                cleave_left.GetComponent<SpriteRenderer>().enabled = true;
                cleave_right.GetComponent<CapsuleCollider2D>().enabled = false;
                cleave_right.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (cleave_logic_timer > cleave_telegraph_timer + 0.6f)
        {
            cleave_warning.SetActive(false);
            if (player_Transform.position.x > transform.position.x)
            {
                cleave_right.GetComponent<CapsuleCollider2D>().enabled = false;
                cleave_right.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                cleave_left.GetComponent<CapsuleCollider2D>().enabled = false;
                cleave_left.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (cleave_logic_timer > cleave_telegraph_timer + 2f)
        {
            cleave_logic_timer = 0f;
        }
    }
    void Cleave_close_package()
    {
        cleave_logic_timer = 0f;
        cleave_warning.SetActive(false);
        cleave_left.GetComponent<CapsuleCollider2D>().enabled = false;
        cleave_left.GetComponent<SpriteRenderer>().enabled = false;
        cleave_left.GetComponent<CapsuleCollider2D>().enabled = false;
        cleave_left.GetComponent<SpriteRenderer>().enabled = false;
    }

}
