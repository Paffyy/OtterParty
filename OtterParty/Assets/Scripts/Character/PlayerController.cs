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
    public Vector2 InputDirection { get; set; }
    public Action OnJumpAction { get; set; }
    public Action OnFireAction { get; set; }
    public Action<Vector2> OnMoveAction { get; set; }
    public Action<bool> OnSpamAction { get; set; }
    public bool IsGrounded { get; set; }

    private Rigidbody playerBody;
    private Vector3 movement;

    protected override void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        base.Awake();
    }

    private new void Update()
    {
        ApplyMovement();
        base.Update();
    }

    private void FixedUpdate()
    {
        playerBody.MovePosition(transform.position + movement * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        if (InputDirection.sqrMagnitude > deadZoneValue)
        {
            movement = new Vector3(InputDirection.x, 0, InputDirection.y) * speed;
            transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        }
        else
        {
            movement = Vector3.zero;
        }
    }

    public void Jump()
    {
        playerBody.velocity += new Vector3(0, jumpHeight, 0);
    }
    public void Jump(float jumpHeightInput)
    {
        playerBody.velocity += new Vector3(0, jumpHeightInput, 0);
    }
    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        OnMoveAction?.Invoke(input);
    }
    private void OnJump()
    {
        OnJumpAction?.Invoke();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
}
