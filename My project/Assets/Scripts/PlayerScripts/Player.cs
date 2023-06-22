using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Scene Starting Position")]
    public static VectorValue startingPosition;
    public VectorValue startingPositionTemp;

    [Header("States")]
    public static bool isDead;
    public bool isDeadTemp;

    [Header("Health")]
    public static int currentHealth;
    public static int maxHealth;
    public int currentHealthTemp;
    public int maxHealthTemp;

    [Header("Mana")]
    public static float currentMana;
    public static float maxMana;
    public float currentManaTemp;
    public float maxManaTemp;

    [Header("Attack Damage")]
    public static int attackDamage;
    public int attackDamageTemp;

    // Abilities
    public enum Abilities { sword, wallCling, dash, fireball, doubleJump };
    public static bool[] unlocked = new bool[Enum.GetNames(typeof(Abilities)).Length];
    public bool[] unlockedTemp = new bool[Enum.GetNames(typeof(Abilities)).Length];

    private void OnEnable()
    {
        startingPosition = startingPositionTemp;
        currentHealth = currentHealthTemp;
        maxHealth = maxHealthTemp;
        currentMana = currentManaTemp;
        maxMana = maxManaTemp;
        unlocked = unlockedTemp;
        attackDamage = attackDamageTemp;
    }

    public static bool unlockCheck(int ability)
    {
        return unlocked[ability];
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
