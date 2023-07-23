using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakableController : MonoBehaviour
{
    [Header("Breakable Objects")]
    // Start
    public static bool[] breakables = new bool[Enum.GetNames(typeof(BreakableEnum)).Length];

    public enum BreakableEnum
    {
        startWall1,
        startWall2, 
        startGround, 
        startHiddenWall1,
        startHiddenWall2,
        startHiddenWall3,
        startHiddenWall4,
        Up2,
        trainShortcut,
        mazeOfDoom,
        UpSecret,
        golemHallway
    }

    public static bool checkBreakable(string referenceName)
    {
        BreakableEnum referenceBreak = (BreakableEnum)Enum.Parse(typeof(BreakableEnum), referenceName);
        return breakables[(int)referenceBreak];
    }

    public static void breakBreakable(string referenceName)
    {
        BreakableEnum referenceBreak = (BreakableEnum) Enum.Parse(typeof(BreakableEnum), referenceName);
        breakables[(int) referenceBreak] = true;
    }
}
