using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public void StartGame()
    {
        SceneManager.LoadScene("B2");
        playerStorage.initialValue = playerPosition;
    }
}
