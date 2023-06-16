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
    // Health
    public int currentHealth;
    public int maxHealth;

    // Soul
    public float currentSoul;
    public float maxSoul;

    // Abilities
    public enum Abilities { sword, wallCling, dash };
    public bool[] unlocked = new bool[Enum.GetNames(typeof(Abilities)).Length];

    // Scene
    public String scene;

    // Position
    public float[] position;

    public PlayerData(Player player)
    {
        currentHealth = Player.currentHealth;
        maxHealth = Player.maxHealth;

        //currentSoul = player.GetComponent<Soul>().currentSouls;
        //maxSoul = player.GetComponent<Soul>().maxSoul;

        unlocked[(int)Abilities.sword] = Player.unlocked[(int)Abilities.sword];
        unlocked[(int)Abilities.wallCling] = Player.unlocked[(int)Abilities.wallCling];
        unlocked[(int)Abilities.dash] = Player.unlocked[(int)Abilities.dash];

        scene = SceneManager.GetActiveScene().name;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}
