using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_moveset : MonoBehaviour
{
    
    
    public Transform player_transform;
    private float timer = 0f;
    public bool isPhase2;
    public int max_golem_health;
    private int current_golem_health;
    public Rigidbody2D golem_rigidbody2D;
    public float golem_speed;
    public Transform golem_transform;


    //Spike
    public GameObject spike;
    public float spike_speed;

    //Log
    public GameObject log;
    private float log_timer = 0f;
    public float log_speed;

    //Dynamic pillar
    public bool have_pillars_spawned = false;
    public GameObject pillar;
    public float pillar_telegraph_spawn_time;
    public bool have_pillar_telegraph_spawned = false;
    public GameObject telegraph_pillar;
    private float pillar_telegraph_spawn_timer;

    //Projectiles
    public GameObject projectile;
    public GameObject Bomb_projectile;
    private int spawnAngle = 0;
    public float projectile_speed;
    public float bomb_projectile_speed;
    public float bomb_projectile_spawn_interval;
    public float projectile_spawn_interval;
    public int consequetive_projectile_angle_degrees;
    private float projectile_spawn_timer = 0;
    private float bomb_projectile_spawn_timer = 0;
    public int bomb_max_bounces;
    public int bouncy_projectile_max_bounces;

    //Attack Booleans
    //private bool isBombAttack1 = false;
    //private bool isCircularAttack1 = false;

    //testing
    private bool aaa = false;

    private void Start()
    {
        golem_rigidbody2D = GetComponent<Rigidbody2D>();
        current_golem_health = max_golem_health;
        pillar_telegraph_spawn_timer = pillar_telegraph_spawn_time;
    }
    void Update()
    {
        timer += Time.deltaTime;


        if (current_golem_health < max_golem_health / 2)
            isPhase2 = true;


        if (20 > timer && timer >= 0f)
        {
            //BombAttack1();
        }
        if (47 > timer && timer >= 27)
        {
            //CircularAttack1();
        }
        if (timer >= 50)
            timer = 0;


        LogAttack();
 
        //SpikeAttack();
        DynamicPillarLogic();
        GolemMovement();
    }

    void CircularAttack1()
    {
        
        projectile_spawn_timer += Time.deltaTime;
        if (projectile_spawn_timer > projectile_spawn_interval)
        {
            projectile_spawn_timer = 0f;
            var Projectile1 = Instantiate(projectile, transform.position, transform.rotation);  //Projectile spawning
            var Projectile2 = Instantiate(projectile, transform.position, transform.rotation);
            float radians = spawnAngle * Mathf.Deg2Rad;
            Vector2 movementDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            Projectile1.GetComponent<Rigidbody2D>().velocity = movementDirection * projectile_speed;
            Projectile2.GetComponent<Rigidbody2D>().velocity = -movementDirection * projectile_speed;
            if (isPhase2) //For the creation of a 3rd and 4th circle of projectiles
            {
                float radiansExtra = (spawnAngle + 90) * Mathf.Deg2Rad; //Essentially same as bullet1 and bullet2 but shifter 90 degrees
                Vector2 movementDirection1 = new Vector2(Mathf.Cos(radiansExtra), Mathf.Sin(radiansExtra));
                var Projectile3 = Instantiate(projectile, transform.position, transform.rotation);
                var Projectile4 = Instantiate(projectile, transform.position, transform.rotation);
                Projectile3.GetComponent<Rigidbody2D>().velocity = movementDirection1 * projectile_speed;
                Projectile4.GetComponent<Rigidbody2D>().velocity = -movementDirection1 * projectile_speed;
            }
            spawnAngle += consequetive_projectile_angle_degrees; //Next projectile spawn angle (to make circular spawning possible)
        }
    }
    void BombAttack1()
    {
        bomb_projectile_spawn_timer += Time.deltaTime;
        if (bomb_projectile_spawn_timer > bomb_projectile_spawn_interval)
        {
            bomb_projectile_spawn_timer = 0f;
            var BombProjectile1 = Instantiate(Bomb_projectile, transform.position, transform.rotation);  //Projectile spawning
            Vector2 movementDirection = new Vector2(player_transform.position.x - transform.position.x, player_transform.position.y - transform.position.y); //Direction to player
            movementDirection.Normalize(); //Made to length 1 so it doesnt affect speed
            BombProjectile1.GetComponent<Rigidbody2D>().velocity = movementDirection * bomb_projectile_speed; //Speed applied
        }
    }

    void GolemMovement()
    {
        Vector2 movementDirection = new Vector2(player_transform.position.x - golem_transform.position.x, player_transform.position.y - golem_transform.position.y); //Direction to player
        movementDirection.Normalize(); //Made to length 1 so it doesnt affect speed
        golem_rigidbody2D.velocity = movementDirection * golem_speed; //Speed applied
    }

    void DynamicPillarLogic()
    {
        if (isPhase2 && !have_pillars_spawned)
        {
            if (!have_pillar_telegraph_spawned)
            {
                var Telegraph_Pillar1 = Instantiate(telegraph_pillar, new Vector2(5, 2), transform.rotation);
                var Telegraph_Pillar2 = Instantiate(telegraph_pillar, new Vector2(-5, 2), transform.rotation);
                var Telegraph_Pillar3 = Instantiate(telegraph_pillar, new Vector2(5, -2), transform.rotation);
                var Telegraph_Pillar4 = Instantiate(telegraph_pillar, new Vector2(-5, -2), transform.rotation);
                Destroy(Telegraph_Pillar1, pillar_telegraph_spawn_timer);
                Destroy(Telegraph_Pillar2, pillar_telegraph_spawn_timer);
                Destroy(Telegraph_Pillar3, pillar_telegraph_spawn_timer);
                Destroy(Telegraph_Pillar4, pillar_telegraph_spawn_timer);
                have_pillar_telegraph_spawned = true;
            }


            pillar_telegraph_spawn_time -= Time.deltaTime;
            if (pillar_telegraph_spawn_time <= 0)
            {
                have_pillars_spawned = true;
                var Pillar1 = Instantiate(pillar, new Vector2(5, 2), transform.rotation);
                var Pillar2 = Instantiate(pillar, new Vector2(-5, 2), transform.rotation);
                var Pillar3 = Instantiate(pillar, new Vector2(5, -2), transform.rotation);
                var Pillar4 = Instantiate(pillar, new Vector2(-5, -2), transform.rotation);
            }
        }
    }

    void SpikeAttack()
    {
        if (timer >= 1f)
        {
            float spike_trajectory_start = Random.Range(-7.5f, 7.5f);
            float spike_trajectory_end = Random.Range(-7.5f, 7.5f);
            var Spike = Instantiate(spike, new Vector2(spike_trajectory_start, 4.5f), transform.rotation);
            Spike.GetComponent<Rigidbody2D>().velocity = new Vector2(spike_trajectory_end, -4.5f * spike_speed); //Direction to player
            timer = 0;
        }
    }
    void LogAttack()
    {
        log_timer += Time.deltaTime;
        if (log_timer >= 5f)
        {
            float log_trajectory_start1 = Random.Range(-7.5f, -2.5f); //First log
            float log_trajectory_end1 = Random.Range(-3f, 3f);
            var Log1 = Instantiate(log, new Vector2(log_trajectory_start1, 4.5f), transform.rotation);
            Log1.transform.rotation = Quaternion.Euler(0f, -9f, log_trajectory_end1 - log_trajectory_start1); //This does z y x in that order for some reason

            float log_trajectory_start2 = Random.Range(-2.5f, 2.5f); //Second log
            float log_trajectory_end2 = Random.Range(2f, 8f);
            var Log2 = Instantiate(log, new Vector2(log_trajectory_start2, 4.5f), transform.rotation);
            Log2.transform.rotation = Quaternion.Euler(0f, -9f, log_trajectory_end2 - log_trajectory_start2); //This does z y x in that order for some reason

            float log_trajectory_start3 = Random.Range(2.5f, 7.5f); //Third log
            float log_trajectory_end3 = Random.Range(-8f, -2f);
            var Log3 = Instantiate(log, new Vector2(log_trajectory_start3, 4.5f), transform.rotation);
            Log3.transform.rotation = Quaternion.Euler(0f, -9f, log_trajectory_end3 - log_trajectory_start3); //This does z y x in that order for some reason


            log_timer = 0;
        }
    }
}
