using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Windows.Speech;

public class Object_test_script : MonoBehaviour
{
    public float speed;
    public float jump_height;
    private Rigidbody2D rigid_body_player;
    public bool isGrounded = true;
    void Start()
    {
        rigid_body_player = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = false;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, jump_height);
        }
        if (Input.GetButtonUp("Jump") && rigid_body_player.velocity.y > -1f)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, rigid_body_player.velocity.y * 0.5f);
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rigid_body_player.velocity = new Vector2(-speed, rigid_body_player.velocity.y);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rigid_body_player.velocity = new Vector2(speed, rigid_body_player.velocity.y);
        }
    }
}