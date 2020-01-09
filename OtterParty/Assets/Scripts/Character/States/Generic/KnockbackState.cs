using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KnockbackState")]
public class KnockbackState : CharacterBaseState
{
    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float stunDuration;
    [SerializeField]
    [Range(1, 100)]
    private float knockbackMagnitude;
    public bool IsKnockedBacked { get; set; }
    private PlayerController.CurrentPlayerState previousState;

    public override void Enter()
    {
        previousState = owner.PlayerState;
        owner.PlayerState = PlayerController.CurrentPlayerState.KnockBackState;
        IsKnockedBacked = false;
        owner.InputDirection = Vector2.zero;
        owner.GetComponent<Rigidbody>().velocity = -owner.transform.forward * knockbackMagnitude * ImportManager.Instance.Settings.ShoveForceMultiplier;
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
