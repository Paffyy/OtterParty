using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform moveableTarget;
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

    void Start()
    {
        ChangeTarget();
    }

    void FixedUpdate()
    {
        moveableTarget.position = Vector3.MoveTowards(moveableTarget.position, newPosition, smooth * Time.deltaTime);
    }

    public void Respawn()
    {
        //todo
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
