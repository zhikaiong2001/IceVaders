using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soulbar : MonoBehaviour
{
    public Image soulBar;
    [SerializeField] private Soul playerSoul;

    // Start is called before the first frame update
    void Start()
    {
        soulBar.fillAmount = playerSoul.currentSouls / 100f;

    }

    // Update is called once per frame
    void Update()
    {
        soulBar.fillAmount = playerSoul.currentSouls / 100f;
    }
}
