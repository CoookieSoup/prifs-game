using System;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20;
    public int damage = 5;
    private float destructionDelay = 3;

    private void Start()
    {
        Destroy(gameObject, destructionDelay);
    }

    private void Update()
    {
        transform.position +=  -transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
