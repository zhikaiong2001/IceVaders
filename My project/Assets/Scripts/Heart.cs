using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public string referenceName;

    void OnEnable()
    {
        if (UnlockableController.checkUnlock(referenceName))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UnlockableController.unlockUnlock(referenceName);
            gameObject.SetActive(false);
            Player.maxHealth += 1;
            Player.currentHealth = Player.maxHealth;
        }
    }
}
