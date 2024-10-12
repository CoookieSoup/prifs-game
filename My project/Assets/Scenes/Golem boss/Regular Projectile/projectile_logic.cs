using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_logic : MonoBehaviour
{
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }

}
