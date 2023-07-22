using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float playerVelocity = 5f;
    [SerializeField] float timeBeforeEnablingMove = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        PlayerMovement move = player.GetComponent<PlayerMovement>();
        Animator anim = player.GetComponent<Animator>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //Debug.Log("working");
        bool isFacingRight = move.isFacingRight;
        anim.SetInteger("state", 1);
        move.enabled = false;
        if (isFacingRight)
        {
            rb.velocity = new Vector2(playerVelocity, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(-playerVelocity, rb.velocity.y);
        }
        
        yield return new WaitForSeconds(timeBeforeEnablingMove);
        move.enabled = true;
        anim.SetInteger("state", 0);
    }
}
