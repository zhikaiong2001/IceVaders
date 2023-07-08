using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneOfRecall : MonoBehaviour
{
    private Player player;
    public HintBox stoneDialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            collision.gameObject.GetComponent<Player>().SavePlayer();
            if (stoneDialogue != null)
            {
                stoneDialogue.gameObject.SetActive(true);
                stoneDialogue.startDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            if (stoneDialogue != null)
            {
                stoneDialogue.gameObject.SetActive(false);
            }
        }
    }
}
