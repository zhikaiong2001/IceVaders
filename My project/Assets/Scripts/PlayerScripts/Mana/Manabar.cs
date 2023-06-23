using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    public Image manaBar;

    // Start is called before the first frame update
    void Start()
    {
        manaBar.fillAmount = Player.currentMana / 100f;

    }

    // Update is called once per frame
    void Update()
    {
        manaBar.fillAmount = Player.currentMana / 100f;
    }
}
