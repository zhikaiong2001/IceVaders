using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGround : MonoBehaviour
{
    public AudioSource shakeGround;
    public AudioSource breakGround;
    public GameObject breakGroundPrefab;
    public PlayerMovement playerMovement;
    public float breakTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        //shakeGround.Play();
        playerMovement.disableMovement();
        yield return new WaitForSeconds(breakTime);
        //shakeGround.Stop();
        //breakGround.Play();
        breakGroundPrefab.SetActive(false);
        playerMovement.enableMovement();
    }
}
