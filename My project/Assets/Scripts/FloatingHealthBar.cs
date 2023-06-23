using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private EnemyHealth enemy;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        if (slider.value == 0)
        {
            Destroy(gameObject);
        }
    }
  

    // Update is called once per frame
    void Update()
    {
        
        UpdateHealthBar((float)enemy.currentHealth, (float)enemy.maxHealth);
        
    }
}
