using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulSkills : MonoBehaviour
{
    private Health playerHealth;
    private Soul playerSoul;

    private bool isHealing = false;
    public int healAmount = 1;
    public float healCost = 40f;
    [SerializeField] private AudioSource healSoundEffect;

    public Transform firePosition;
    public GameObject projectile;
    public float fireBallCost = 50f;
    public int fireBallDamage = 100;
    [SerializeField] private AudioSource fireballSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
        playerSoul = GetComponent<Soul>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //Healing();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            FireBall();
        }

    }

    /*private void Healing()
    {
        if (playerSoul.currentSouls >= healCost)
        {
            playerHealth.heal(healAmount);
            playerSoul.UseSoul(healCost);
            healSoundEffect.Play();
        }
    }*/

    private void FireBall()
    {
        if(playerSoul.currentSouls >= fireBallCost)
        {
            Instantiate(projectile, firePosition.position, transform.rotation);
            playerSoul.UseSoul(fireBallCost);
            fireballSoundEffect.Play();
        }
        
    }
}
