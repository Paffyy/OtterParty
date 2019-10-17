using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

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
    [SerializeField]
    private int maxSpeed;
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
        if(move.sqrMagnitude > 0.3f)
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

    void Jump()
    {
        Debug.Log("Jumping");
        playerBody.velocity = new Vector3(movement.x, jumpHeight, movement.z);
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
