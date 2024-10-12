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
    void Start()
    {
        rigid_body_player = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); //Did not use GetAxis, because it has values besides 0 1 -1 and that makes the movement slugish
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (moveVertical != 0 && moveHorizontal != 0)
        {
            moveVertical /= Mathf.Sqrt(2);  //Needed else player moves sqrt(2) * speed while moving diagonally which feels unnatural
            moveHorizontal /= Mathf.Sqrt(2);
        }
        rigid_body_player.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed); //Apply movement
    }
}
