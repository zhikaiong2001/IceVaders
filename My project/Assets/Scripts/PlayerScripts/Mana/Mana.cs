using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public void useMana(float amount)
    {
        Player.currentMana = Mathf.Clamp(Player.currentMana - amount, 0, Player.maxMana);
    }

    public void gainMana(float amount)
    {
        Player.currentMana = Mathf.Clamp(Player.currentMana + amount, 0, Player.maxMana);
    }
}
