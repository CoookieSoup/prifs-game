using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_pillar_script : MonoBehaviour
{
    private float timer = 0f;
    private Golem_moveset golem_script;
    private void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
        if (collision2D.gameObject.CompareTag("Player") && GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            GetComponent<Health>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > golem_script.pillar_crumble_timer)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().excludeLayers |= LayerMask.GetMask("Player");
            if (timer > golem_script.pillar_crumble_timer + 4f)
            {
                Destroy(gameObject);
            }
        }
    }
}
