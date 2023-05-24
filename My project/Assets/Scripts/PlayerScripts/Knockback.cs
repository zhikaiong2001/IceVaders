using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float KBForce, KBCounter, KBTotalTime;

    private bool KnockFromRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KBCounter <= 0)
        {
            return;
        } 
        else
        {
            if (!KnockFromRight)
            {
                Vector2 force = new Vector2(-KBForce, KBForce);
                Debug.Log(force);
                rb.velocity = force;
            }
            else
            {
                rb.velocity = new Vector2 (KBForce, KBForce);
            }
        }

        KBCounter -= Time.deltaTime;
    }
}
