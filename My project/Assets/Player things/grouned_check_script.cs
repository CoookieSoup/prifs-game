using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grouned_check_script : MonoBehaviour
{
    private Player_script player_script;
    void Start()
    {
        player_script = FindObjectOfType<Player_script>();
        player_script.isGrounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            player_script.isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            player_script.isGrounded = false;
        }
    }
}
