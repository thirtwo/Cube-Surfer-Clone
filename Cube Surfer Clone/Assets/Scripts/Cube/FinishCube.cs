using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCube : Cube
{
    public int finishMultiplier;
    private bool isHit = false;
    public bool IsHit
    {
        get { return isHit; }
        set
        {
            GameManager.scoreMultiplier = finishMultiplier;
            isHit = value;
        }
    }
}
