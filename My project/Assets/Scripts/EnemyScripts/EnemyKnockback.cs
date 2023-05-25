using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private float KBForceHor;
    [SerializeField] private float KBForceVer;
    [SerializeField] private float KBCounter;
    [SerializeField] private float KBTotalTime;
    [SerializeField] private float StunDuration;
    [SerializeField] private float StunTotal;
    [SerializeField] private bool KnockFromRight;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
