using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    [Header("States")]
    public bool isDeadTemp;

    [Header("Health")]
    public int currentHealthTemp;
    public int maxHealthTemp;

    [Header("Mana")]
    public float currentManaTemp;
    public float maxManaTemp;

    [Header("Attack Damage")]
    public int attackDamageTemp;

    // Abilities
    public enum Abilities { sword, wallCling, dash, fireball, doubleJump };
    public static bool[] unlocked = new bool[Enum.GetNames(typeof(Abilities)).Length];
    public bool[] unlockedTemp = new bool[Enum.GetNames(typeof(Abilities)).Length];

    // Scene
    public String scene;

    // Position
    public float[] position;

    public PlayerData(Player player)
    {
        isDeadTemp = Player.isDead;

        currentHealthTemp = Player.currentHealth;
        maxHealthTemp = Player.maxHealth;

        currentManaTemp = Player.currentMana;
        maxManaTemp = Player.maxMana;

        attackDamageTemp = Player.attackDamage;

        unlocked[(int)Abilities.sword] = Player.unlocked[(int)Abilities.sword];
        unlocked[(int)Abilities.wallCling] = Player.unlocked[(int)Abilities.wallCling];
        unlocked[(int)Abilities.dash] = Player.unlocked[(int)Abilities.dash];
        unlocked[(int)Abilities.fireball] = Player.unlocked[(int)Abilities.fireball];
        unlocked[(int)Abilities.doubleJump] = Player.unlocked[(int)Abilities.doubleJump];

        scene = SceneManager.GetActiveScene().name;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}
