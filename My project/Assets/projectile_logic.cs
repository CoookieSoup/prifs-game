using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_logic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.gameObject.tag != "Boss" && collision.gameObject.tag != "Projectile")
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
