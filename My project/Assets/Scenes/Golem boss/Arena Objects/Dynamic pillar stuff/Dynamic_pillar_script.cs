using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_pillar_script : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
