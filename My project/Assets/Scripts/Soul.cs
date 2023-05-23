using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float startingSouls;
<<<<<<< Updated upstream
    [SerializeField] private float soulsPerAttack;
=======
   // [SerializeField] private float soulsPerAttack;
>>>>>>> Stashed changes
    public float currentSouls;

    // Start is called before the first frame update
    void Start()
    {
        currentSouls = startingSouls;
<<<<<<< Updated upstream
        
=======

>>>>>>> Stashed changes
    }
    public void UseSoul(float amount)
    {
        currentSouls -= amount;
        currentSouls = Mathf.Clamp(currentSouls, 0, 100);
    }

    public void GainSoul(float amount)
    {
        currentSouls += amount;
        currentSouls = Mathf.Clamp(currentSouls, 0, 100);
    }

}
