using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBox : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float rotationSpeed;
    [SerializeField]
    private Transform rotatingBox;
    [SerializeField]
    private Material material;
    [SerializeField]
    private MeshRenderer mesh;

    void Start()
    {
        if (material != null)
        {
            mesh.material = material;
        }
    }

    void FixedUpdate()
    {
        rotatingBox.Rotate(new Vector3(1, 0, 0), rotationSpeed * ImportManager.Instance.Settings.RotationMultiplier);
    }
}
