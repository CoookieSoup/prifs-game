using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
    private bool isGroundedCopy = false;
    public bool isDead = false;
    private Health health;
    
    public bool spawnFacingLeft;
    private bool isFacingLeft;
    private Vector2 facingLeft;

    public float move_horizontal;

    public Animator animator;

    public AudioClip jumpSound;
    public AudioClip landingSound;
    public AudioClip playerDeathSound;
    //public AudioClip playerTakeDamageSound;
    public AudioClip stepSound;

    public float stepSoundInterval = 0.1f;
    private float stepIntervalCopy;

    private float timer = 0f;
    private float take_damage_timer;
    public float iframe_time = 0.5f;
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
        
        stepIntervalCopy = stepSoundInterval;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile" && timer - take_damage_timer > iframe_time)
        {
            take_damage_timer = timer;
            health.TakeDamage(10); // test amount of damage
        }
        if (collision.collider.tag == "Boss" && timer - take_damage_timer > iframe_time)
        {
            take_damage_timer = timer;
            health.TakeDamage(30);
        }
        //Audio.Play(playerTakeDamageSound);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hurtbox") && timer - take_damage_timer > iframe_time)
        {
            take_damage_timer = timer;
            health.TakeDamage(10);
        }
        if (other.CompareTag("Projectile") && timer - take_damage_timer > iframe_time)
        {
            take_damage_timer = timer;
            health.TakeDamage(10);
        }
        //Audio.Play(playerTakeDamageSound);
    }
    void Update()
    {
        timer += Time.deltaTime;
        move_horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rigid_body_player.velocity = new Vector2(rigid_body_player.velocity.x, jump_height);
            Audio.Play(jumpSound);
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

        if (isGrounded && !isGroundedCopy)
        {
            Audio.Play(landingSound);
        }
        isGroundedCopy = isGrounded;
        
        if (health.hp <= 0f)
        {
            animator.Play("PlayerDeathReal");
            if (!isDead)
                Audio.Play(playerDeathSound);
            isDead = true;
            rigid_body_player.velocity = Vector2.zero;

        }
        else if (move_horizontal == 0f && isGrounded)
        {
            animator.Play("Idle");
            stepIntervalCopy = stepSoundInterval;
        }
        else
        {
            animator.Play("Walk");
            stepIntervalCopy -= Time.deltaTime;
            if (stepIntervalCopy <= 0)
            {
                Audio.Play(stepSound);
                stepIntervalCopy = stepSoundInterval;
            }
        }
    }
    protected virtual void Flip()
    {
        if (!isDead)
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
}