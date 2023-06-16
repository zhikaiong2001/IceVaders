using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Scene Starting Position
    public static VectorValue startingPosition;
    public VectorValue startingPositionTemp;

    // Health
    public static int currentHealth;
    public static int maxHealth;
    public int currentHealthTemp;
    public int maxHealthTemp;

    // Soul
    public static float currentSoul;
    public static float maxSoul;
    public float currentSoulTemp;
    public float maxSoulTemp;

    // Attack Damage
    public static int attackDamage;
    public int attackDamageTemp;

    // Abilities
    public enum Abilities { sword, wallCling, dash, fireball };
    public static bool[] unlocked = new bool[Enum.GetNames(typeof(Abilities)).Length];
    public bool[] unlockedTemp = new bool[Enum.GetNames(typeof(Abilities)).Length];

    private void Start()
    {
        startingPosition = startingPositionTemp;
        currentHealth = currentHealthTemp;
        maxHealth = maxHealthTemp;
        currentSoul = currentSoulTemp;
        maxSoul = maxSoulTemp;
        unlocked = unlockedTemp;
    }


    // Save and Load
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        startingPosition.initialValue = position;
        Debug.Log(position.ToString());

        SceneManager.LoadScene(data.scene);

        currentHealth = data.currentHealth;
        maxHealth = data.maxHealth;

        //currentSoul = player.GetComponent<Soul>().currentSouls;
        //maxSoul = player.GetComponent<Soul>().maxSoul;

        unlocked[(int)Abilities.sword] = data.unlocked[(int)Abilities.sword];
        unlocked[(int)Abilities.wallCling] = data.unlocked[(int)Abilities.wallCling];
        unlocked[(int)Abilities.dash] = data.unlocked[(int)Abilities.dash];

        transform.position = position;
    }
}
