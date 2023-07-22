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

    public string referenceName;

    void OnEnable()
    {
        if (BreakableController.checkBreakable(referenceName))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            BreakableController.breakBreakable(referenceName);
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
