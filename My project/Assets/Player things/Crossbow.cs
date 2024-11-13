using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public float shootCooldown;
    private float cooldownCopy;
    
    public GameObject boltPrefab;
    
    public AudioClip shootSound;

    private Camera cam;
    
    private void Start()
    {
        cooldownCopy = shootCooldown;
        cam = Camera.main;
    }

    private void Update()
    {
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
