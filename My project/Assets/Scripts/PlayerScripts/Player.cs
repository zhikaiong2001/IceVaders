using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Difficulty
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    public static Difficulty difficulty;
    public Difficulty difficultyTemp;
    public static float[] difficulyMult = { 0.75f, 1.0f, 1.25f };

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
    public enum Abilities 
    { 
        sword, 
        wallCling, 
        dash, 
        fireball, 
        doubleJump 
    };
    public static bool[] unlocked = new bool[Enum.GetNames(typeof(Abilities)).Length];
    public bool[] unlockedTemp = new bool[Enum.GetNames(typeof(Abilities)).Length];

    // Last Save
    public Vector2 savePos;

    // Variable Control
    private static bool tempted = false;

    private void OnEnable()
    {
            startingPosition = startingPositionTemp;
            currentHealth = currentHealthTemp;
            maxHealth = maxHealthTemp;
            currentMana = currentManaTemp;
            maxMana = maxManaTemp;
            unlocked = unlockedTemp;
            attackDamage = attackDamageTemp;
            tempted = true;
    }

    // Abilities Unlocked Getter
    public static bool unlockCheck(int ability)
    {
        return unlocked[ability];
    }

    // Difficulty Getter
    public static float getDiffMult()
    {
        if (difficulty == Difficulty.Easy)
        {
            return difficulyMult[0];
        }
        else if (difficulty == Difficulty.Normal)
        {
            return difficulyMult[1];
        }
        else if (difficulty == Difficulty.Hard)
        {
            return difficulyMult[2];
        }
        else
        {
            return 10f;
        }
    }

    // Save and Load
    public void SavePlayer()
    {
        savePos = transform.position;
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        startingPosition.initialValue = position;

        SceneManager.LoadScene(data.scene);

        isDead = false;

        currentHealth = data.currentHealthTemp;
        maxHealth = data.maxHealthTemp;

        currentMana = data.currentManaTemp;
        maxMana = data.maxManaTemp;

        attackDamage = data.attackDamageTemp;

        unlocked[(int)Abilities.sword] = data.unlockedTemp[(int)Abilities.sword];
        unlocked[(int)Abilities.wallCling] = data.unlockedTemp[(int)Abilities.wallCling];
        unlocked[(int)Abilities.dash] = data.unlockedTemp[(int)Abilities.dash];
        unlocked[(int)Abilities.fireball] = data.unlockedTemp[(int)Abilities.fireball];
        unlocked[(int)Abilities.doubleJump] = data.unlockedTemp[(int)Abilities.doubleJump];

        transform.position = position;
    }
}
