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
    [Header("Spike Respawn Location")]
    public static Vector2 spikeRespawn;

    [Header("States")]
    public bool isDeadTemp;
    public bool paybackActiveTemp;

    // Difficulty
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    public Difficulty difficultyTemp;
    public static float[] difficulyMult = { 2f, 1.0f, 0.5f };

    [Header("Health")]
    public int currentHealthTemp;
    public int maxHealthTemp;

    [Header("Mana")]
    public float currentManaTemp;
    public float maxManaTemp;

    [Header("Attack Damage")]
    public int attackDamageTemp;

    // Abilities
    public enum Abilities
    {
        sword,
        wallCling,
        dash,
        fireball,
        doubleJump,
        payback
    };
    public static bool[] unlocked = new bool[Enum.GetNames(typeof(Abilities)).Length];
    public bool[] unlockedTemp = new bool[Enum.GetNames(typeof(Abilities)).Length];

    // Scene
    public String scene;

    // Position
    public float[] position;

    public PlayerData(Player player)
    {
        spikeRespawn = Player.spikeRespawn;

        isDeadTemp = Player.isDead;
        paybackActiveTemp = Player.paybackActive;

        difficultyTemp = (Difficulty) Player.difficulty;

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
        unlocked[(int)Abilities.payback] = Player.unlocked[(int)Abilities.payback];

        scene = SceneManager.GetActiveScene().name;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}
