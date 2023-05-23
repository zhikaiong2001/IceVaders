using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulSkills : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Soul playerSoul;

    private bool isHealing = false;
    public float healAmount = 1f;
    public float healCost = 40f;
    public float healTime = 2f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !isHealing)
        {
            Healing();
        }

    }

    private void Healing()
    {
        if (playerSoul.currentSouls >= healCost)
        {
            isHealing = true;
            playerHealth.Heal(healAmount);
            playerSoul.UseSoul(healCost);
            isHealing = false;
        } 
    }
}
