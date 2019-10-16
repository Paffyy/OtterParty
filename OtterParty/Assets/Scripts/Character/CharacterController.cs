using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    private Rigidbody rigidbody;
    private TestController controls;
    private Vector2 move;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;

    void Awake()
    {
        Debug.Log("Awake");
        rigidbody = GetComponent<Rigidbody>();
        controls = new TestController();

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
  
            Vector3 movement = new Vector3(move.x, 0, move.y) * Time.deltaTime * speed;
        // rigidbody.AddForce(movement);
        transform.Translate(movement, Space.World);
            if (movement != Vector3.zero)
            {
                rigidbody.rotation = Quaternion.LookRotation(movement);
            }

    }

    void Jump()
    {
        Debug.Log("Jumping");
        rigidbody.velocity = new Vector3(0, jumpHeight, 0); 
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
