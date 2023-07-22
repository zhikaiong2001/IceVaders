using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int strikesToBreak;
    public GameObject attachment;

    [Header("Sound")]
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
        //breakSound.Play();
        strikesToBreak -= 1;

        if (strikesToBreak == 0)
        {
            BreakableController.breakBreakable(referenceName);
            gameObject.SetActive(false);
            if (attachment != null)
            {
                attachment.SetActive(false);
            }
        }
    }
}
