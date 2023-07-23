using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelMove : MonoBehaviour
{
    public string sceneToLoad;
    [SerializeField] private Vector2 playerPosition;
    //[SerializeField] Animator transition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.useStartingPosition = true;
            Player.startingPosition.initialValue = playerPosition;
            StartCoroutine(LoadLevel());
            Debug.Log(Player.startingPosition.initialValue);
        }
    }

    IEnumerator LoadLevel()
    {
        //transition.SetTrigger("End");
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneToLoad);
        //transition.SetTrigger("Start");
    }
}
