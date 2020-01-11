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
    [SerializeField]
    private Material material;
    [SerializeField]
    private MeshRenderer meshOne;
    private Vector3 rotation;

    enum axisEnum
    {
        X,
        Y,
        Z
    };

    [SerializeField]
    private axisEnum rotateOnAxis;

    void Start()
    {
        if (material != null)
        {
            meshOne.material = material;
        }
        if (rotateOnAxis == axisEnum.X)
        {
            rotation = new Vector3(rotatingDirection, 0, 0);
        }
        else if (rotateOnAxis == axisEnum.Y)
        {
            rotation = new Vector3(0, rotatingDirection, 0);
        }
        else if (rotateOnAxis == axisEnum.Z)
        {
            rotation = new Vector3(0, 0, rotatingDirection);
        }
    }

    void FixedUpdate()
    {
        rotatingCross.Rotate(rotation, rotationSpeed * ImportManager.Instance.Settings.RotationMultiplier);
    }
}
