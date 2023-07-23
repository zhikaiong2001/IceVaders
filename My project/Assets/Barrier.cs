using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public EnemyHealth bossHealth;

    private void Update()
    {
        if(bossHealth.isDead)
        {
            gameObject.SetActive(false);
        }
    }
    
}
