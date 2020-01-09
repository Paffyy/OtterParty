using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/LockedMovementState")]
public class LockedMovementState : CharacterBaseState
{
    [SerializeField]
    [Range(2f, 2.8f)]
    private float speed;
    [SerializeField]
    [Range(0.5f, 0.85f)]
    private float variationPenalty;
    [SerializeField]
    [Range(2.4f, 6f)]
    private float maxSpeed;
    [SerializeField]
    [Range(1f, 10f)]
    private float jumpHeight;
    private Rigidbody playerBody;
    private bool previousButtonPressed;


    public override void Enter()
    {
        owner.PlayerState = PlayerController.CurrentPlayerState.LockedMovementState;
        owner.IsInLockedMovement = true;
        playerBody = owner.GetComponent<Rigidbody>();
        owner.OnSpamAction += SpamAction;
        owner.OnJumpAction += JumpAction;
        base.Enter();
    }

    private void SpamAction(bool isRightButtonPressed)
    {
        float spamSpeed = previousButtonPressed != isRightButtonPressed ? speed : speed * variationPenalty;
        ApplyMovement(new Vector3(0, 0, spamSpeed * ImportManager.Instance.Settings.RunAndJumpModifier));
        previousButtonPressed = isRightButtonPressed;
    }

    private void ApplyMovement(Vector3 movement)
    {
        float updatedSpeed = Vector3.ProjectOnPlane((playerBody.velocity + movement), Vector3.up).magnitude;

        if (updatedSpeed > maxSpeed * ImportManager.Instance.Settings.RunAndJumpModifier)
        {
            playerBody.velocity = playerBody.velocity.normalized * maxSpeed * ImportManager.Instance.Settings.RunAndJumpModifier;
        }
        else
        {
            playerBody.velocity += movement;
        }
    }

    private void JumpAction()
    {
        owner.Jump(jumpHeight);
        owner.Transition<LockedAirState>();
    }

    public override void Exit()
    {
        owner.IsInLockedMovement = false;
        owner.OnSpamAction -= SpamAction;
        owner.OnJumpAction -= JumpAction;
    }
}
