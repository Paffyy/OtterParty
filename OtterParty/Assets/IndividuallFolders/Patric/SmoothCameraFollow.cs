using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 cameraOffset;
    [SerializeField]
    private float cameraFollowSmoothness;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + cameraOffset, cameraFollowSmoothness);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, cameraFollowSmoothness);

    }

    public void SetTarget(Transform transform)
    {
        target = transform;
    }
}
