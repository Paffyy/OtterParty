using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine
{

    [SerializeField]
    public float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private int maxSpeed;
    [SerializeField]
    [Range(0.1f, 0.3f)]
    private float deadZoneValue;
    [SerializeField]
    [Range(2f, 7f)]
    private float fallMultiplier;
    public Vector2 InputDirection { get; set; }
    public Action OnJumpAction { get; set; }
    public Action OnFireAction { get; set; }
    public Action OnShoveAction { get; set; }
    public Action<Vector2> OnMoveAction { get; set; }
    public Action<bool> OnSpamAction { get; set; }
    public bool IsOnMovingPlatform { get; set; }
    public bool IsInLockedMovement { get; set; }
    public bool IsActive { get; set; }
    public Transform Parent { get; set; }
    public CapsuleCollider BodyCollider { get { return bodyCollider; } }
    [SerializeField]
    private CapsuleCollider bodyCollider;
    private Rigidbody playerBody;
    public Rigidbody PlayerBody { get { return playerBody; } }
    private Vector3 movement;
    [SerializeField]
    private Transform firePoint;
    public Transform FirePoint { get { return firePoint; } }
    [SerializeField]
    private GameObject gun;
    public GameObject PlayerGun { get { return gun; } }
    private Animator anim;
    public Animator Anim { get { return anim; } }
    public SkinnedMeshRenderer MeshRenderer { get { return meshRen; } }
    [SerializeField]
    private SkinnedMeshRenderer meshRen;
    private bool hasReceivedInput;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private float velocityThreshold;
    [SerializeField]
    private Transform hatPlaceHolder;
    public Transform HatPlaceHolder { get { return hatPlaceHolder; } set { hatPlaceHolder = value; } }


    public CurrentPlayerState PlayerState { get; set; }

    public enum CurrentPlayerState
    {
        MovingState,
        OnMovingPlatformState,
        KnockBackState,
        AirState,
        LockedMovementState,
        LockedAirState,
        ShootingState,
        LockedKnockBackState
    }

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        Parent = transform.parent;
        IsInLockedMovement = false;
        IsOnMovingPlatform = false;
        playerBody = GetComponent<Rigidbody>();

        base.Awake();
    }

    private new void Update()
    {
        AdjustJumpBehaviour();
        ApplyMovement();
        HandleWalkingAnimation();
        base.Update();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (IsOnMovingPlatform)
        {
            playerBody.velocity = movement;
        }
        else
        {
            playerBody.MovePosition(transform.position + movement * Time.deltaTime);
        }
        CheckIfNoMovement();
    }

    private void AdjustJumpBehaviour()
    {
        if (playerBody.velocity.y < 1.5f)
        {
            playerBody.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            playerBody.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier / 1.5f - 1) * Time.deltaTime;
        }
    }

    private void CheckIfNoMovement()
    {
        if (playerBody.velocity.magnitude < velocityThreshold)
        {
            playerBody.velocity = Vector3.zero;
        }
    }

    private void HandleWalkingAnimation()
    {
        if (hasReceivedInput || (IsInLockedMovement && playerBody.velocity.z > 0.1f))
        {
            if (anim != null)
                anim.SetBool("IsWalking", true);
        }
        else
        {
            if (anim != null)
                anim.SetBool("IsWalking", false);
        }
    }

    private void ApplyMovement()
    {
        if (InputDirection.sqrMagnitude > deadZoneValue)
        {
            hasReceivedInput = true;
            movement = new Vector3(InputDirection.x, 0, InputDirection.y) * speed;
            transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        }
        else
        {
            hasReceivedInput = false;
            movement = Vector3.zero;
        }
    }

    public void Jump()
    {
        if (anim != null)
            anim.SetTrigger("Jump");
        playerBody.velocity += new Vector3(0, jumpHeight, 0);
    }
    public void Jump(float jumpHeightInput)
    {
        if (anim != null)
            anim.SetTrigger("Jump");
        playerBody.velocity += new Vector3(0, jumpHeightInput, 0);
    }
    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        OnMoveAction?.Invoke(input);
    }
    private void OnJump()
    {
        if (IsGrounded())
        {
            OnJumpAction?.Invoke();
        }
    }
    private void OnLeftSpam()
    {
        OnSpamAction?.Invoke(false);
    }
    private void OnRightSpam()
    {
        OnSpamAction?.Invoke(true);
    }
    private void OnFire()
    {
        OnFireAction?.Invoke();
    }

    private void OnShove()
    {
        OnShoveAction?.Invoke();
    }

    public bool IsGrounded()
    {
        var point1 = transform.position + new Vector3(0, bodyCollider.height - bodyCollider.radius, 0);
        var point2 = transform.position + new Vector3(0, bodyCollider.radius, 0);
        Physics.CheckSphere(transform.position + new Vector3(0, bodyCollider.radius - 2 * groundCheckDistance, 0), bodyCollider.radius - groundCheckDistance, collisionMask);
        if (Physics.CheckSphere(transform.position + new Vector3(0, bodyCollider.radius - 2 * groundCheckDistance, 0), bodyCollider.radius - groundCheckDistance, collisionMask))
        {
            return true;
        }
        return false;
    }
}
