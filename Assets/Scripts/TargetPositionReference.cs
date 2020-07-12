using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPositionReference : MonoBehaviour
{
    public static Vector3 TargetPosition;

    public void Update()
    {
        TargetPositionReference.TargetPosition = this.transform.position;
    }
}
