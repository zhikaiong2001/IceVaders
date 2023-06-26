using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private Player player;
    public HintBox dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            dialogue.gameObject.SetActive(true);
            dialogue.startDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            dialogue.gameObject.SetActive(false);
        }
    }
}
