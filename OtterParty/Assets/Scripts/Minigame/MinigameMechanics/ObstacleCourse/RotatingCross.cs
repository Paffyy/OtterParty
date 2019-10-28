using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCross : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float rotationSpeed;
    [SerializeField]
    private Transform rotatingCross;
    [SerializeField]
    [Range(-1, 1)]
    private int rotatingDirection;

    void FixedUpdate()
    {
        rotatingCross.Rotate(new Vector3(0, rotatingDirection, 0), rotationSpeed);
    }
}
