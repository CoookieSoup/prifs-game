using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    public int maxHp;

    void Start()
    {
        maxHp = 100;
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