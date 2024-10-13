using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar_script : MonoBehaviour
{
    public Golem_moveset golem_script;
    // Start is called before the first frame update
    void Start()
    {
        golem_script = FindObjectOfType<Golem_moveset>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
