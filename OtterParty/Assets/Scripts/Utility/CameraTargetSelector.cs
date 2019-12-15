using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private FollowCameraScript followCamera;
    private List<Transform> cameraSnapPoints = new List<Transform>();
    void Awake()
    {
        foreach (Transform item in transform)
        {
            cameraSnapPoints.Add(item);
        }
    }
    void Start()
    {
        followCamera.SetTarget(cameraSnapPoints[0]);
    }
}
