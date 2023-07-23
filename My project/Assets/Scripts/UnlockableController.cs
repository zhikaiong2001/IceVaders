using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableController : MonoBehaviour
{
    public static bool[] unlocks = new bool[Enum.GetNames(typeof(Unlocks)).Length];

    public enum Unlocks
    {
        dash,
        wallcling,
        fireball,
        doublejump,
        payback,
        heart1,
        heart2,
        heart3,
        heart4,
        sword1,
        sword2,
        sword3
    }

    public static bool checkUnlock(string referenceName)
    {
        Unlocks referenceBreak = (Unlocks)Enum.Parse(typeof(Unlocks), referenceName);
        return unlocks[(int)referenceBreak];
    }

    public static void unlockUnlock(string referenceName)
    {
        Unlocks referenceBreak = (Unlocks)Enum.Parse(typeof(Unlocks), referenceName);
        unlocks[(int)referenceBreak] = true;
    }
}
