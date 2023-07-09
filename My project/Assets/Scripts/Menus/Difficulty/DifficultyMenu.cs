using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{
    public Player.Difficulty difficulty;

    public void changeDifficulty()
    {
        Player.difficulty = this.difficulty;
    }
}
