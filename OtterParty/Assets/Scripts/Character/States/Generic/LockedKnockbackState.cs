using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/LockedKnockbackState")]
public class LockedKnockbackState : CharacterBaseState
{
    [SerializeField]
    [Range(0.5f, 2.0f)]
    private float stunDuration;
    private bool shouldTransition;
    public override void Enter()
    {
        owner.PlayerState = PlayerController.CurrentPlayerState.LockedKnockBackState;
        //owner.InputDirection = Vector2.zero;
        owner.IsInLockedMovement = true;
        shouldTransition = false;
        owner.GetComponent<Rigidbody>().velocity = -owner.transform.forward * Constants.Instance.DefaultKnockbackDistance;
        base.Enter();
        Task.Factory.StartNew(async () =>
        {
            await Task.Delay((int)(stunDuration * 1000));
            shouldTransition = true;
        });

    }
    public override void HandleLateUpdate()
    {
        base.HandleLateUpdate();
        if (shouldTransition)
        {
            owner.Transition<LockedMovementState>();
        }
    }

    public override void Exit()
    {
        owner.IsInLockedMovement = false;
    }
}
