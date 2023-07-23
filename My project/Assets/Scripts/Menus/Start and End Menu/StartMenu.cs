using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public void NewGame()
    {
        Player.useStartingPosition = true;
        Player.startingPosition.initialValue = playerPosition;
        SceneManager.LoadScene("Start");
    }

    public void StartTutorial()
    {
        playerStorage.initialValue = playerPosition;
        Player.startingPosition = playerStorage;
        SceneManager.LoadScene("Tutorial");
    }

    public void SelectDifficulty()
    {
        SceneManager.LoadScene("Difficulty Menu");
    }
}
