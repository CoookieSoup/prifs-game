using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool have_started_phase2 = false;

    private Animator animator;

    //Spike
    public GameObject spike;
    public float spike_speed;

    //Log
    public GameObject log;
    private float log_timer = 0f;
    public float log_speed;
    public bool do_despawn_logs = false;
    private float alternative_log_timer_phase2 = 0f;

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

    private void Start()
    {
        golem_rigidbody2D = GetComponent<Rigidbody2D>();
        current_golem_health = max_golem_health;
        pillar_telegraph_spawn_timer = pillar_telegraph_spawn_time;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        animator.Play("Golem_idle");
        if (current_golem_health < max_golem_health / 2)
            isPhase2 = true;
        if (!isPhase2)
        {
            if (timer <= 0f)
            {
                GolemMovement();
            }
            if (timer >= 0f && timer <= 30f)
            {
                BombAttack1();
                GolemMovement();
            }
            if (timer >= 30f && timer <= 40f)
            {
                GolemMovement();
            }
            if (timer >= 40f && timer <= 60f)
            {
                CircularAttack1();
                GolemMovement();
            }
            if (timer >= 60f && timer <= 70f)
            {
                Golem_return_home();
            }
            if (timer >= 66f && timer <= 96f)
            {
                LogAttack();
            }
            if (timer >= 100f) //Restart the cycle
            {
                log_timer = 0f;
                timer = -5f;
                do_despawn_logs = true;
            }
        }
        if (isPhase2)
        {
            if (!have_started_phase2)
            {
                timer = -5f;
                have_started_phase2 = true;
                golem_speed *= 1.3f;
            }
            DynamicPillarLogic();
            if (timer >= -5f && timer <= 0f)
            {
                GolemMovement();
            }
            if (timer >= 0f && timer <= 30f)
            {
                BombAttack1();
                GolemMovement();
            }
            if (timer >= 30f && timer <= 40f)
            {
                GolemMovement();
            }
            if (timer >= 40f && timer <= 60f)
            {
                CircularAttack1();
                GolemMovement();
            }
            if (timer >= 60f && timer <= 65f)
            {
                Golem_return_home();
            }
            if (timer >= 66f && timer <= 85f)
            {
                LogAttack();
            }
            if (timer >= 85f && timer <= 86f)
            {
                log_timer = 0f;
            }
            if (timer >= 86f && timer <= 110f)
            {
                LogAttack();
                Golem_return_home();
            }
            if (timer >= 115f) //Restart the cycle
            {
                log_timer = 0f;
                timer = -5f;
                do_despawn_logs = true;
            }
        }
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
        /*
        Vector2 direction = (player_transform.position - golem_transform.position);
        float distance = Vector2.Distance(golem_transform.position, player_transform.position);

        RaycastHit2D hit = Physics2D.Raycast(golem_transform.position, direction);
        if (hit.collider.CompareTag("Player"))
        {
            Debug.Log("Hit object: " + hit.collider.name);
        }
        Debug.DrawRay(golem_transform.position, direction);
        */


        log_timer += Time.deltaTime;
        alternative_log_timer_phase2 += Time.deltaTime;
        if (log_timer >= 0.1f && log_timer <= 4f)
        {
            do_despawn_logs = false;
            float log_trajectory_start1 = Random.Range(-15f, -5f); //First vertical log
            float log_trajectory_end1 = Random.Range(5f, 25f);
            var Log1 = Instantiate(log, new Vector2(log_trajectory_start1, 15f), transform.rotation);
            Vector2 direction1 = new Vector2(log_trajectory_end1 - log_trajectory_start1, -35f);
            direction1.Normalize();
            Log1.GetComponent<Rigidbody2D>().velocity = direction1 * log_speed;
            Log1.transform.rotation = Quaternion.Euler(0f, -20f, log_trajectory_end1 - log_trajectory_start1); //This does z y x in that order for some reason

            float log_trajectory_start2 = Random.Range(-5f, 5f); //Second vertical log
            float log_trajectory_end2 = Random.Range(-15f, 15f);
            var Log2 = Instantiate(log, new Vector2(log_trajectory_start2, 15f), transform.rotation);
            Vector2 direction2 = new Vector2(log_trajectory_end2 - log_trajectory_start2, -35f);
            direction2.Normalize();
            Log2.GetComponent<Rigidbody2D>().velocity = direction2 * log_speed;
            Log2.transform.rotation = Quaternion.Euler(0f, -20f, log_trajectory_end2 - log_trajectory_start2); //This does z y x in that order for some reason

            float log_trajectory_start3 = Random.Range(5f, 15f); //Third vertical log
            float log_trajectory_end3 = Random.Range(-25f, -5f);
            var Log3 = Instantiate(log, new Vector2(log_trajectory_start3, 15f), transform.rotation);
            Vector2 direction3 = new Vector2(log_trajectory_end3 - log_trajectory_start3, -35f);
            direction3.Normalize();
            Log3.GetComponent<Rigidbody2D>().velocity = direction3 * log_speed;
            Log3.transform.rotation = Quaternion.Euler(0f, -20f, log_trajectory_end3 - log_trajectory_start3); //This does z y x in that order for some reason

            if (isPhase2)
            {
                //2 more vertical logs
                float log_trajectory_start8 = Random.Range(-9f, -5f); //Fourth vertical log
                float log_trajectory_end8 = Random.Range(-8f, -3f);
                var Log8 = Instantiate(log, new Vector2(log_trajectory_start8, 15f), transform.rotation);
                Vector2 direction8 = new Vector2(log_trajectory_end8 - log_trajectory_start8, -35f);
                direction8.Normalize();
                Log8.GetComponent<Rigidbody2D>().velocity = direction8 * log_speed;
                Log8.transform.rotation = Quaternion.Euler(0f, -20f, log_trajectory_end8 - log_trajectory_start8); //This does z y x in that order for some reason

                float log_trajectory_start9 = Random.Range(5f, 9f); //Fifth vertical log
                float log_trajectory_end9 = Random.Range(3f, 8f);
                var Log9 = Instantiate(log, new Vector2(log_trajectory_start9, 15f), transform.rotation);
                Vector2 direction9 = new Vector2(log_trajectory_end9 - log_trajectory_start9, -35f);
                direction9.Normalize();
                Log9.GetComponent<Rigidbody2D>().velocity = direction9 * log_speed;
                Log9.transform.rotation = Quaternion.Euler(0f, -20f, log_trajectory_end9 - log_trajectory_start9); //This does z y x in that order for some reason
            }
            log_timer = 5f;
            alternative_log_timer_phase2 = 5f;
        }
        if (isPhase2 && alternative_log_timer_phase2 >= 9.5f)
        {


            //2 horizontal logs
            float log_trajectory_start6 = Random.Range(-3f, 4f); //First horizontal log
            float log_trajectory_end6 = Random.Range(-4f, 4f);
            var Log6 = Instantiate(log, new Vector2(-25f, log_trajectory_start6), transform.rotation);
            Vector2 direction6 = new Vector2(30f, log_trajectory_end6 - log_trajectory_start6);
            direction6.Normalize();
            Log6.GetComponent<Rigidbody2D>().velocity = direction6 * log_speed;
            Log6.transform.rotation = Quaternion.Euler(0f, 0f, 90 + Mathf.Atan((log_trajectory_end6 - log_trajectory_start6) / 15f)); //This does z y x in that order for some reason
            Log6.transform.localScale = new Vector3(Log6.transform.localScale.x, Log6.transform.localScale.y / 3, Log6.transform.localScale.z);
            Log6.GetComponent<log_script>().is_horizontal = true;

            float log_trajectory_start7 = Random.Range(-3f, 4f); //Second horizontal log
            float log_trajectory_end7 = Random.Range(-4f, 4f);
            var Log7 = Instantiate(log, new Vector2(25f, log_trajectory_start7), transform.rotation);
            Vector2 direction7 = new Vector2(-30f, log_trajectory_end7 - log_trajectory_start7);
            direction7.Normalize();
            Log7.GetComponent<Rigidbody2D>().velocity = direction7 * log_speed;
            Log7.transform.rotation = Quaternion.Euler(0f, 0f, -90 - Mathf.Atan((log_trajectory_end7 - log_trajectory_start7) / 15f)); //This does z y x in that order for some reason
            Log7.transform.localScale = new Vector3(Log7.transform.localScale.x, Log7.transform.localScale.y / 3, Log7.transform.localScale.z);
            Log7.GetComponent<log_script>().is_horizontal = true;
            alternative_log_timer_phase2 = 6.5f;
        }
        if (log_timer >= 8f)
        {
            float log_trajectory_start4 = Random.Range(-3f, 4f); //First horizontal log
            float log_trajectory_end4 = Random.Range(-4f, 4f);
            var Log4 = Instantiate(log, new Vector2(-25f, log_trajectory_start4), transform.rotation);
            Vector2 direction4 = new Vector2(30f, log_trajectory_end4 - log_trajectory_start4);
            direction4.Normalize();
            Log4.GetComponent<Rigidbody2D>().velocity = direction4 * log_speed;
            Log4.transform.rotation = Quaternion.Euler(0f, 0f, 90 + Mathf.Atan((log_trajectory_end4 - log_trajectory_start4) / 15f)); //This does z y x in that order for some reason
            Log4.transform.localScale = new Vector3(Log4.transform.localScale.x, Log4.transform.localScale.y / 3, Log4.transform.localScale.z);
            Log4.GetComponent<log_script>().is_horizontal = true;

            float log_trajectory_start5 = Random.Range(-3f, 4f); //Second horizontal log
            float log_trajectory_end5 = Random.Range(-4f, 4f);
            var Log5 = Instantiate(log, new Vector2(25f, log_trajectory_start5), transform.rotation);
            Vector2 direction5 = new Vector2(-30f, log_trajectory_end5 - log_trajectory_start5);
            direction5.Normalize();
            Log5.GetComponent<Rigidbody2D>().velocity = direction5 * log_speed;
            Log5.transform.rotation = Quaternion.Euler(0f, 0f, -90 - Mathf.Atan((log_trajectory_end4 - log_trajectory_start5) / 15f)); //This does z y x in that order for some reason
            Log5.transform.localScale = new Vector3(Log5.transform.localScale.x, Log5.transform.localScale.y / 3, Log5.transform.localScale.z);
            Log5.GetComponent<log_script>().is_horizontal = true;

            log_timer = 5f;
        }

    }

    void Golem_return_home()
    {
        if (golem_transform.position.x < 7f)
        {
            Vector2 movementDirection = new Vector2(7f - golem_transform.position.x, 0f - golem_transform.position.y); //Direction to player
            movementDirection.Normalize(); //Made to length 1 so it doesnt affect speed
            golem_rigidbody2D.velocity = movementDirection * golem_speed * 3; //Speed applied
        }
    }
}
