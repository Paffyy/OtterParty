using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 cameraOffset;
    [SerializeField]
    private float cameraFollowSmoothness;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + cameraOffset, cameraFollowSmoothness);
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
    }
}
