using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    private void Awake()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            NextLine();
        }
    }

    public void startDialogue()
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
        }
    }
}
