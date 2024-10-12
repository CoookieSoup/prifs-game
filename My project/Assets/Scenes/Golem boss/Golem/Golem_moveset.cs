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

    private bool have_pillars_spawned = false;
    public GameObject pillar;
    public float pillar_spawn_telegraph_timer = 0;
    public bool have_pillar_telegraph_spawned = false;

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

    //Attack Booleans
    //private bool isBombAttack1 = false;
    //private bool isCircularAttack1 = false;

    private void Start()
    {
        golem_rigidbody2D = GetComponent<Rigidbody2D>();
        current_golem_health = max_golem_health;
    }
    void Update()
    {
        if (current_golem_health < max_golem_health/2)
            isPhase2 = true;
        timer += Time.deltaTime;

        if (20 > timer && timer >= 0f)
        {
            BombAttack1();
        }
        if (47 > timer && timer >= 27)
        {
            CircularAttack1();
        }
        GolemMovement();
        if (isPhase2 && !have_pillars_spawned)
        {
            /*
            if (!have_pillar_telegraph_spawned)
            {
                var Telegraph_Pillar1 = Instantiate(telegraph_pillar, new Vector2(5, 2), transform.rotation);
                var Telegraph_Pillar2 = Instantiate(telegraph_pillar, new Vector2(-5, 2), transform.rotation);
                var Telegraph_Pillar3 = Instantiate(telegraph_pillar, new Vector2(5, -2), transform.rotation);
                var Telegraph_Pillar4 = Instantiate(telegraph_pillar, new Vector2(-5, -2), transform.rotation);
                have_pillar_telegraph_spawned = true;
            }
            */
            
            pillar_spawn_telegraph_timer += Time.deltaTime;
            if (pillar_spawn_telegraph_timer >= 5)
            {
                have_pillars_spawned = true;
                var Pillar1 = Instantiate(pillar, new Vector2(5, 2), transform.rotation);
                var Pillar2 = Instantiate(pillar, new Vector2(-5, 2), transform.rotation);
                var Pillar3 = Instantiate(pillar, new Vector2(5, -2), transform.rotation);
                var Pillar4 = Instantiate(pillar, new Vector2(-5, -2), transform.rotation);
                //Destroy(Telegraph_Pillar1, Telegraph_Pillar2, Telegraph_Pillar3, Telegraph_Pillar4);
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
}
