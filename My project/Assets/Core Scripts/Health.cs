using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    public int maxHp = 100;

    void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("YOU DIED");
        // for the future
    }
}
