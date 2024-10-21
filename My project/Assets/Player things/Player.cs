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
    public bool jump_is_primed = true;
    public float jump_prime_timer = 0f;
    void Start()
    {
        rigid_body_player = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && rigid_body_player.velocity.y == 0f)
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
        jump_prime_timer += Time.deltaTime;
        //float move_horizontal = Input.GetAxisRaw("Horizontal");
        //float move_vertical = Input.GetAxisRaw("vertical");
        /*if (move_horizontal != 0)
        {
            rigid_body_player.velocity = new Vector2(move_horizontal * speed, rigid_body_player.velocity.y);
        }
        if (move_vertical != 0 && isGrounded)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, move_vertical * jump_height);
        }
        */
        if (Input.GetButtonDown("Jump") && isGrounded || isGrounded && jump_is_primed)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, jump_height);
            jump_is_primed = false;
        }
        if (Input.GetButtonUp("Jump") && rigid_body_player.velocity.y > -1f)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, rigid_body_player.velocity.y); //*0.5f later TESTING
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rigid_body_player.velocity = new Vector2(-speed, rigid_body_player.velocity.y);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rigid_body_player.velocity = new Vector2(speed, rigid_body_player.velocity.y);
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x * 0.5f, rigid_body_player.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && !jump_is_primed)
        {
            jump_is_primed = true;
        }
        if (!Input.GetKey(KeyCode.Space) && jump_is_primed)
        {
            jump_is_primed = false;
        }
    }
}