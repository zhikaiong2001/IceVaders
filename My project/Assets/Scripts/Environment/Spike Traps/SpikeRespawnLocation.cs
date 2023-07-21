using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRespawnLocation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.spikeRespawn = this.transform.position;
    }
}
