using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    private Rigidbody playerBody;
    private TestController controls;
    private Vector2 move;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    private Vector3 movement;

    void Awake()
    {
        Debug.Log("Awake");
        playerBody = GetComponent<Rigidbody>();
        controls = new TestController();

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void Start()
    {

    }

    void Update()
    {
        movement = new Vector3(move.x, playerBody.velocity.y, move.y) * speed;
        transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
    }

    void FixedUpdate()
    { 
        playerBody.velocity = movement;
    }

    void Jump()
    {
        Debug.Log("Jumping");
        //playerBody.velocity += new Vector3(0, jumpHeight, 0);
        playerBody.AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
