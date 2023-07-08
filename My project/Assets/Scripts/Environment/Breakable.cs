using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int strikesToBreak;

    [Header("Sound")]
    public AudioSource breakSound;

    public void breakObject()
    {
        //breakSound.Play();
        strikesToBreak -= 1;

        if (strikesToBreak == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
