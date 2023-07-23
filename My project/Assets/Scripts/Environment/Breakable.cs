using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int strikesToBreak;
    public GameObject attachment;

    [Header("Sound")]
    public AudioSource weakenSound;
    public AudioSource breakSound;

    public string referenceName;

    void OnEnable()
    {
        if (BreakableController.checkBreakable(referenceName))
        {
            gameObject.SetActive(false);
        }
    }

    public void breakObject()
    {
        weakenSound.Play();
        strikesToBreak -= 1;

        if (strikesToBreak == 0)
        {
            breakSound.Play();
            BreakableController.breakBreakable(referenceName);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (attachment != null)
            {
                attachment.SetActive(false);
            }
        }
    }
}
