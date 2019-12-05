using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/ImprovedKnockbackState")]
public class ImprovedKnockBackState : CharacterBaseState
{
    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float stunDuration;
    [SerializeField]
    [Range(1, 100)]
    private float horizontalknockbackMagnitude;
    [SerializeField]
    [Range(1, 100)]
    private float verticalKnockbackMagnitude;
    public bool IsKnockedBacked { get; set; }
    private PlayerController.CurrentPlayerState previousState;

    public override void Enter()
    {
        previousState = owner.PlayerState;
        owner.PlayerState = PlayerController.CurrentPlayerState.KnockBackState;
        IsKnockedBacked = false;
        owner.InputDirection = Vector2.zero;
        owner.GetComponent<Rigidbody>().velocity = -owner.transform.forward * horizontalknockbackMagnitude + Vector3.up * verticalKnockbackMagnitude;
        base.Enter();
        Cooldown.Instance.StartNewCooldown(stunDuration, this);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (IsKnockedBacked)
        {
            if (previousState == PlayerController.CurrentPlayerState.OnMovingPlatformState)
                owner.Transition<OnMovingPlatformState>();
            else
                owner.Transition<MovingState>();
        }
    }
}
