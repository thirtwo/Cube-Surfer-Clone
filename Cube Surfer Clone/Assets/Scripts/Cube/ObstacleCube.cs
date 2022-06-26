using UnityEngine;

public class ObstacleCube : Cube
{
    private bool isHit = false;
    public bool IsHit { get { return isHit; } set { isHit = value; } }
}
