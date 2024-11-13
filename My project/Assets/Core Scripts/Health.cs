using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public int hp;
    public int maxHp = 100;
    
    
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        Audio.Play(takeDamageSound);
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector3 pos = transform.transform.position + randomVector;

        GameObject popup = Instantiate(damagePopupPrefab, pos, Quaternion.identity);
        popup.GetComponent<DamageIndicator>().Setup(dmg);
        if (hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Audio.Play(deathSound);
        Debug.Log("YOU DIED");
        // for the future
    }
}
