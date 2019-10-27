﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : ActivePlatform
{
    [SerializeField]
    [Range(0.1f, 3.0f)]
    private float rotationSpeed;
    [SerializeField]
    [Range(-1, 1)]
    private int rotationDirection;
    [SerializeField]
    private Transform rotatingPlatform;

    private void FixedUpdate()
    {
        rotatingPlatform.Rotate(new Vector3(0, rotationDirection, 0), rotationSpeed);
    }
}
