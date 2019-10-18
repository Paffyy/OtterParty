using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine
{

    private Rigidbody playerBody;
    private TestController controls;
    private Vector2 move;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    private Vector3 movement;
    [SerializeField]
    private int maxSpeed;

    protected override void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        controls = new TestController();
        base.Awake();
    }

    void Start()
    {

    }

    void Update()
    {
        if(move.sqrMagnitude > 0.2f)
        {
            movement = new Vector3(move.x, 0, move.y) * speed;
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
        //playerBody.velocity = movement;
    }

    public void Jump()
    {
        playerBody.velocity += new Vector3(0, jumpHeight, 0);
    }


    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    private void OnJump()
    {
        Jump();
    }
}
