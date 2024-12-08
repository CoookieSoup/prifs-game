using Unity.VisualScripting;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public float shootCooldown;
    private float cooldownCopy;
    
    public GameObject boltPrefab;
    
    public AudioClip shootSound;

    private Camera cam;
    public Player_script player_Script;

    public SpriteRenderer sprite;
    public Melee melee;
    
    private void Start()
    {
        player_Script = FindObjectOfType<Player_script>();
        cooldownCopy = shootCooldown;
        cam = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
        melee = FindObjectOfType<Melee>();
    }

    private void Update()
    {
        if (melee.hide_crossbow)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }
        if (player_Script.isDead)
        {
            Destroy(gameObject);
        }
        if (cooldownCopy > 0) cooldownCopy -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownCopy <= 0)
        {
            cooldownCopy = shootCooldown;
            Instantiate(boltPrefab, transform.position, transform.rotation);
            Audio.Play(shootSound);
        }

        // Look at mouse
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf. Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad + 90; // Offset this by 90 Degrees
        
        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        Debug.DrawLine(transform.position, mousePos, Color.white, Time.deltaTime);
    }
}
