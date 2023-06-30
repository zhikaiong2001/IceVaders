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
        SceneManager.LoadScene("Mountain of Giants Start");
        playerStorage.initialValue = playerPosition;
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        playerStorage.initialValue = playerPosition;
    }

    public void SelectDifficulty()
    {
        SceneManager.LoadScene("Difficulty Menu");
    }
}
