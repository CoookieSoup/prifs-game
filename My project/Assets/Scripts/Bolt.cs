using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20;
    public int damage = 5;
    private float destructionDelay = 1.5f;

    private void Start()
    {
        Destroy(gameObject, destructionDelay);
    }

    private void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        if (!other.CompareTag("Boss")) return;
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
