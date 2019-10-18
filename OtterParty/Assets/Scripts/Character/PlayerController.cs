using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine
{

    public Rigidbody playerBody;
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
    public Action OnJumpAction;
    public Action<Vector2> OnMoveAction;
    public Action OnLeftSpamAction;
    public Action OnRightSpamAction;
    private Vector3 movement;

    protected override void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        base.Awake();
    }

    void Update()
    {
        if(InputDirection.sqrMagnitude > deadZoneValue)
        {
            movement = new Vector3(InputDirection.x, 0, InputDirection.y) * speed;
            transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        }
        else
        {
            movement = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        playerBody.MovePosition(transform.position + movement * Time.deltaTime);
    }

    public void Jump()
    {
        playerBody.velocity += new Vector3(0, jumpHeight, 0);
    }
    private void OnMove(InputValue value)
    {
        var move = value.Get<Vector2>();
        OnMoveAction?.Invoke(move);
    }
    private void OnJump()
    {
        OnJumpAction?.Invoke();
    }
    private void OnLeftSpam()
    {
        OnLeftSpamAction?.Invoke();
    }
    private void OnRightSpam()
    {
        OnRightSpamAction?.Invoke();
    }
}
