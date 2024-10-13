using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar_telegraph_script : MonoBehaviour
{
    public Golem_moveset golem_script;
    void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
    }

    // Update is called once per frame
    void Update()
    {
        if(golem_script.pillar_spawn_telegraph_timer <= 0)
        {
            Destroy(this);
        }
    }
}
