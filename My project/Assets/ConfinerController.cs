using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerController : MonoBehaviour
{
    public GameObject newConfiner;
    public GameObject oldConfiner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        newConfiner.SetActive(true);
        oldConfiner.SetActive(false);
    }
}
