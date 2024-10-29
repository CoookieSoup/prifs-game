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
    private Health health;
    
    public bool spawnFacingLeft;
    private bool isFacingLeft;
    private Vector2 facingLeft;
    void Start()
    {
        health = GetComponent<Health>();
        rigid_body_player = GetComponent<Rigidbody2D>();
        
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        if(spawnFacingLeft)
        {
            transform.localScale = facingLeft;
            isFacingLeft = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile") health.TakeDamage(10); // test amount of damage
        if (collision.collider.tag == "Boss") health.TakeDamage(30);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hurtbox")) health.TakeDamage(10);
        if (other.CompareTag("Projectile")) health.TakeDamage(10);
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
        //if (moveDirection != Vector2.zero) transform.up = -moveDirection; // Rotate player in the direction of movement
        if(move_horizontal > 0 && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
        }
        if(move_horizontal < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
        }
    }
    protected virtual void Flip()
    {
        if (isFacingLeft)
        {
            transform.localScale = facingLeft;
        }
        if (!isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}