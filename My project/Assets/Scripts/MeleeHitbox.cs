using System;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public int damage = 10;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Boss")) return;
        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }

    void Update()
    {
        animator.Play("Melee");
    }
}