using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHiddenArea : MonoBehaviour
{
    public GameObject HiddenArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HiddenArea.SetActive(false);
        }
    }
}
