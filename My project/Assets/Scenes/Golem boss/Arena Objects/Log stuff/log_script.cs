using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log_script : MonoBehaviour
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
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * golem_script.log_speed, transform.localScale.z);
    }
}
