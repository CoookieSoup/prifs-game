using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Object_test_script : MonoBehaviour
{
    public float speed; //Can be changed in the Unity editor
    private Rigidbody2D rigid_body_player;
    // Start is called before the first frame update
    void Start()
    {
        rigid_body_player = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (moveVertical != 0 && moveHorizontal != 0)
        {
            moveVertical /= Mathf.Sqrt(2);
            moveHorizontal /= Mathf.Sqrt(2);
        }
        rigid_body_player.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }
}
