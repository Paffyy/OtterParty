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

    void Awake()
    {
        Debug.Log("Awake");
        rigidbody = GetComponent<Rigidbody>();
        controls = new TestController();

        controls.Gameplay.Grow.performed += ctx => Grow();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void Start()
    {

    }

    void Update()
    {
        Vector3 movement = new Vector3(move.x, 0, move.y) * Time.deltaTime * speed;
        rigidbody.AddForce(movement);
       // transform.Translate(movement, Space.World);
    }

    void Grow()
    {
        Debug.Log("Growing");
        transform.localScale *= 1.1f;
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
