using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaybackSkull : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        Player.unlocked[(int)Player.Abilities.payback] = true;
    }
}
