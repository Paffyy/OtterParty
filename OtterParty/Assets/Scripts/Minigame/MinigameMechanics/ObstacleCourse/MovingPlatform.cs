using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : ActivePlatform
{
    [SerializeField]
    [Range(0.1f, 3.0f)]
    private float movingSpeed;
    [SerializeField]
    private Transform firstPosition;
    [SerializeField]
    private Transform secondPosition;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
