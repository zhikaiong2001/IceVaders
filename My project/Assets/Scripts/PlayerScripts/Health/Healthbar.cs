using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        maxHealth = Player.maxHealth;
        totalHealthBar.fillAmount = maxHealth / 10;
    }

    private void Update()
    {
        currentHealth = Player.currentHealth;
        currentHealthBar.fillAmount = currentHealth / 10;

    }

}
