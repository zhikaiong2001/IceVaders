using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBTime;
    [HideInInspector] public bool fromRight;
    private bool isStunned;

    private Rigidbody2D rb;

    public PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void knockCheck()
    {
        if (Player.isDead || isStunned)
        {
            return;
        }
        else
        {
            StartCoroutine(knock());
        }
    }

    private IEnumerator knock()
    {
        isStunned = true;
        playerMovement.disableMovement();
        yield return null;
    }
}
