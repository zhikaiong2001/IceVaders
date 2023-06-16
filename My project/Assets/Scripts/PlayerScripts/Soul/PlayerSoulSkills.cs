using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulSkills : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Soul playerSoul;

    private bool isHealing = false;
    public int healAmount = 1;
    public float healCost = 40f;
    public float healTime = 2f;

    public Transform firePosition;
    public GameObject projectile;
    public float fireBallCost = 50f;
    public int fireBallDamage = 100;


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

        if(Input.GetKeyDown(KeyCode.C))
        {
            FireBall();
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

    private void FireBall()
    {
        if(playerSoul.currentSouls >= fireBallCost)
        {
            Instantiate(projectile, firePosition.position, transform.rotation);
            playerSoul.UseSoul(fireBallCost);
        }
        
    }
}
