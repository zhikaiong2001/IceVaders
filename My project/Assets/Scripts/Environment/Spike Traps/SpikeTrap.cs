using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private GameObject player;
    private Health playerHealth;
    public int spikeDamage;
    public float stunTime;
    public AudioSource knockbackSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.GetComponent<PlayerCollisions>().player;
            playerHealth = player.GetComponent<Health>();

            playerHealth.TakeDamage(spikeDamage);
            player.GetComponent<PlayerMovement>().disableMovement();
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //knockbackSound.Play();
            StartCoroutine(stun());
        }
    }

    private IEnumerator stun()
    {
        yield return new WaitForSeconds(stunTime);
        if (!Player.isDead)
        {
            player.transform.position = Player.spikeRespawn;
            player.GetComponent<PlayerMovement>().enableMovement();
        }
    }
}
