using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float startingSouls;
    
    public float currentSouls;
    public float maxSoul;

    // Start is called before the first frame update
    void Start()
    {
        currentSouls = startingSouls;

    }
    public void UseSoul(float amount)
    {
        currentSouls -= amount;
        currentSouls = Mathf.Clamp(currentSouls, 0, maxSoul);
    }

    public void GainSoul(float amount)
    {
        currentSouls += amount;
        currentSouls = Mathf.Clamp(currentSouls, 0, maxSoul);
    }
}
