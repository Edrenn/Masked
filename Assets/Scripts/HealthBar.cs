using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void HpReachesZero();

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarImage;
    [SerializeField] float infectiousDmg = 0.01f;
    Slider healthBarSlider;
    public bool takeInfectiousDamage = false;
    public HpReachesZero hpReachesZero_Event;

    void Start()
    {
        healthBarSlider = GetComponent<Slider>();
    }

    void Update()
    {
        if (takeInfectiousDamage)
        {
            TakeDamage(infectiousDmg * Time.deltaTime);
        }
    }

    public void TakeDamage(float value)
    {
        healthBarSlider.value -= value;
        UpdateHealthBarColor();
        if (healthBarSlider.value <= 0)
        {
            hpReachesZero_Event();
        }
    }

    void UpdateHealthBarColor()
    {
        if (healthBarSlider.value >= 0.5f)
        {
            float redValue = (1 - healthBarSlider.value) * 2;
            healthBarImage.color = new Color(redValue, 1, healthBarImage.color.b);
        }
        else
        {
            float greenValue = healthBarSlider.value * 2f;
            healthBarImage.color = new Color(1, greenValue, healthBarImage.color.b);
        }
    }
}
