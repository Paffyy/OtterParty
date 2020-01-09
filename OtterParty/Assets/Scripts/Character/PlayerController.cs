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
    [SerializeField]
    [Range(0.5f, 5f)]
    private float invulnerabilityTimer;
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
    [SerializeField]
    private Transform paintPoint;
    public Transform PaintPoint { get { return paintPoint; } }
    [SerializeField]
    private GameObject paintTool;
    public GameObject PlayerPaintTool { get { return paintTool; } }
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
    public bool IsVulnerable { get; set; }
    private MeshRenderer hatMesh;
    [SerializeField]
    private List<MeshRenderer> gunMesh;
    [SerializeField]
    private bool isJoinPlayer;
    [SerializeField]
    private AudioClip playerGotHitSound;
    [SerializeField]
    private float playerGotHitSoundVolume;


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
        LockedKnockBackState,
        PaintingState
    }

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        Parent = transform.parent;
        IsInLockedMovement = false;
        IsOnMovingPlatform = false;
        IsVulnerable = true;
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
            movement = new Vector3(InputDirection.x, 0, InputDirection.y) * speed * ImportManager.Instance.Settings.PlayerSpeedMultiplier;
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
        playerBody.velocity += new Vector3(0, jumpHeight * ImportManager.Instance.Settings.JumpHeightMultiplier, 0);
    }
    public void Jump(float jumpHeightInput)
    {
        if (anim != null)
            anim.SetTrigger("Jump");
        playerBody.velocity += new Vector3(0, jumpHeightInput * ImportManager.Instance.Settings.JumpHeightMultiplier, 0);
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
    private void OnReadyUp()
    {
        ReadyUp();
    }
    private void OnCancelReadyUp()
    {
        ReadyUp();
    }
    private void ReadyUp()
    {
        if (EventHandler.Instance != null && !isJoinPlayer)
        {
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
    }

  
    public void SetInvulnerable()
    {
        IsVulnerable = false;
        StartCoroutine("StartHitAnimation");
        Cooldown.Instance.StartNewCooldown(invulnerabilityTimer, this);
    }

    IEnumerator StartHitAnimation()
    {
        hatMesh = hatPlaceHolder.GetComponentInChildren<MeshRenderer>();
        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityTimer)
        {
            SetMeshVisibility(false);
            yield return new WaitForSeconds(0.075f);
            SetMeshVisibility(true);
            yield return new WaitForSeconds(0.075f);
            elapsedTime += 0.15f;
        }
        SetMeshVisibility(true);
    }

    private void SetMeshVisibility(bool shouldBeVisible)
    {
        meshRen.enabled = shouldBeVisible;
        SetHatMeshVisibility(shouldBeVisible);
        SetGunMeshVisibility(shouldBeVisible);
    }
    private void SetGunMeshVisibility(bool shouldBeVisible)
    {
        if (!shouldBeVisible)
        {
            foreach (MeshRenderer mesh in gunMesh)
            {
                if (mesh != null)
                    mesh.enabled = false;
            }
        }
        else
        {
            foreach (MeshRenderer mesh in gunMesh)
            {
                if (mesh != null)
                    mesh.enabled = true;
            }
        }
    }

    private void SetHatMeshVisibility(bool shouldBeVisibile)
    {
        if (!shouldBeVisibile)
        {
            if (hatMesh != null)
                hatMesh.enabled = false;
        }
        else
        {
            if (hatMesh != null)
                hatMesh.enabled = true;
        }
    }

    public void PlayHitSound()
    {
        if(playerGotHitSound != null)
        {
            SoundEventInfo sei = new SoundEventInfo(playerGotHitSound, playerGotHitSoundVolume, 1);
            EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        }
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
