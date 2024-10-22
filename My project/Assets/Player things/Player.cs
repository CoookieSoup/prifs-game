using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Windows.Speech;

public class Player_script : MonoBehaviour
{
    public float speed;
    public float jump_height;
    private Rigidbody2D rigid_body_player;
    public bool isGrounded = false; //Logic for this is in grounded_check_script
    void Start()
    {
        rigid_body_player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move_horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, jump_height);
        }
        if (Input.GetButtonUp("Jump") && rigid_body_player.velocity.y > -1f)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, rigid_body_player.velocity.y * 0.5f); //* 0.5f so that jump height depends on how long jump was pressed
        }
        if (move_horizontal != 0)
        {
            rigid_body_player.velocity = new Vector2(speed * move_horizontal, rigid_body_player.velocity.y);
        }
        if (move_horizontal == 0)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.y * 0.1f * Time.deltaTime, rigid_body_player.velocity.y); //* 0.1f a
        }
    }
}