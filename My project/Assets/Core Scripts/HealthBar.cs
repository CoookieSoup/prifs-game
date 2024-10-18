using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = health.maxHp;
        slider.minValue = 0;
    }

    
    void Update()
    {
        slider.value = health.hp;
    }
}
