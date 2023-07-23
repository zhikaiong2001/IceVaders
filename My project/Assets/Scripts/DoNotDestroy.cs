using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        
        if(this.gameObject.tag == "Game Music")
        {
            GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Game Music");
            if (musicObj.Length > 1)
            {
                Destroy(this.gameObject);
            }
            
            
        }
        if (this.gameObject.tag == "Boss Music")
        {
            GameObject[] bossObj = GameObject.FindGameObjectsWithTag("Boss Music");
            if (bossObj.Length > 1)
            {
                Destroy(this.gameObject);
            }


        }
        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        if (this.gameObject.name == "BGM Manager")
        {
            return;
        }
        GameObject[] golem = GameObject.FindGameObjectsWithTag("Golem");
        GameObject[] fireApostle = GameObject.FindGameObjectsWithTag("FireApostle");
        if (golem.Length > 0 || fireApostle.Length > 0)
        {
            if (this.gameObject.tag == "Game Music")
            {
                this.gameObject.GetComponent<AudioSource>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<AudioSource>().enabled = true;
            }


        }
        else
        {
            if (this.gameObject.tag == "Boss Music")
            {
                this.gameObject.GetComponent<AudioSource>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<AudioSource>().enabled = true;
            }
        }
    }
}

    
