using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/LockedMovementState")]
public class LockedMovementState : CharacterBaseState
{
    [SerializeField]
    private float fastSpeed;
    [SerializeField]
    private float slowSpeed;
    private bool otherButtonPressed;
    private Vector3 movement;


    public override void Enter()
    {
        owner.speed = 0;
        Debug.Log("Enter LockedMovementState");
        owner.OnLeftSpamAction += LeftBumper;
        owner.OnRightSpamAction += RightBumper;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        owner.playerBody.MovePosition(owner.transform.position + movement * Time.deltaTime);
    }

    private void LeftBumper()
    {
        Debug.Log("LeftBumperPressed");
        if(otherButtonPressed == false)
        {
            movement = new Vector3(0, 0, fastSpeed);
            otherButtonPressed = true;
        } else
        {
            movement = new Vector3(0, 0, slowSpeed);
        }
    }

    private void RightBumper()
    {
        Debug.Log("RightBumperPressed");
        if (otherButtonPressed == true)
        {
            movement = new Vector3(0, 0, fastSpeed);
            otherButtonPressed = false;
        }
        else
        {
            movement = new Vector3(0, 0, slowSpeed);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting LockedMovementState");
        owner.OnLeftSpamAction -= LeftBumper;
        owner.OnRightSpamAction -= RightBumper;
    }
}
