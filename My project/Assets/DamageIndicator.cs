using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public TMP_Text textMesh;      
    public float floatSpeed = 1f;     
    public float fadeSpeed = 2f;     
    private Color textColor;

    void Start()
    {
        textColor = textMesh.color;
    }

    void Update()
    {

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        textColor.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = textColor;

        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }
}
