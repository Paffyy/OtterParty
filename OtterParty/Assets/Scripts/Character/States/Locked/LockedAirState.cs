using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/LockedAirState")]
public class LockedAirState : CharacterBaseState
{
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private float skinWidth;

    public override void Enter()
    {
        owner.IsInLockedMovement = true;
        owner.IsGrounded = false;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.IsGrounded)
        {
            owner.Transition<LockedMovementState>();
        }
    }

    private void CheckCollision()
    {
        float distance = owner.GetComponent<MeshFilter>().mesh.bounds.extents.y + skinWidth;
        Debug.Log(distance);
        Physics.Raycast(owner.transform.position, Vector3.down, out RaycastHit hit, distance, collisionMask);
        if(hit.collider != null)
        {
            owner.Transition<LockedMovementState>();
        }
    }

    public override void Exit()
    {
        owner.IsInLockedMovement = false;
    }
}
