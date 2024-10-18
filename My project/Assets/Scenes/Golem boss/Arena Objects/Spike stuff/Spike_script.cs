using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_script : MonoBehaviour
{
    private float lifetime = 0;
    void Update()
    {
        lifetime += Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Boss") || collision2D.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision2D.gameObject.CompareTag("Wall") && lifetime >= 0.2f)
        {
            Destroy(gameObject);
        }

    }
}
