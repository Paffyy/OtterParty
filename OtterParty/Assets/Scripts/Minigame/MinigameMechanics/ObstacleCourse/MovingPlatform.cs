using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : ActivePlatform
{
    [SerializeField]
    private Transform movingPlatform;
    [SerializeField]
    private Transform firstPosition;
    [SerializeField]
    private Transform secondPosition;
    private Vector3 newPosition;
    private string currentState;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private float resetTime;
    [SerializeField]
    private Material material;
    [SerializeField]
    private MeshRenderer mesh;

    void Start()
    {
        if(material != null)
        {
            mesh.material = material;
        }
        ChangeTarget();
    }

    void FixedUpdate()
    {
        movingPlatform.position = Vector3.MoveTowards(movingPlatform.position, newPosition, smooth * Time.deltaTime);
    }

    private void ChangeTarget()
    {
        if (currentState == "Moving to position 1")
        {
            currentState = "Moving to position 2";
            newPosition = secondPosition.position;
        }
        else if (currentState == "Moving to position 2")
        {
            currentState = "Moving to position 1";
            newPosition = firstPosition.position;
        }
        else if (currentState == "" || currentState == null)
        {
            currentState = "Moving to position 2";
            newPosition = secondPosition.position;
        }
        Invoke("ChangeTarget", resetTime);
    }
}
