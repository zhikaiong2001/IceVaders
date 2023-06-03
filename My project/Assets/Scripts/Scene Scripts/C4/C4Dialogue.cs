using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class C4Dialogue : MonoBehaviour
{
    public AttackOrb attackOrb;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool inDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        if (B2Static.firstTime)
        {
            B2Static.firstTime = false;
            textComponent.text = string.Empty;
            Time.timeScale = 0f;
            startDialogue();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            NextLine();
        }
    }

    void startDialogue()
    {
        index = 0;
        textComponent.text = lines[index];
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = lines[index];
        }
        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
