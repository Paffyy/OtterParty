using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolPlatform : MonoBehaviour
{
    private MeshRenderer mesh;
    private Animator anim;
    private Rigidbody body;
    private bool isFalling;
    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float fallSpeed;
    public bool IsSafe { get; set; }
    public bool HasSymbol { get; set; }
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();

    }

    public void SetSymbol(Material material)
    {
        HasSymbol = true;
        mesh.material = material;
    }

    public void FallDown()
    {
        body.useGravity = true;
        body.velocity += Vector3.down * fallSpeed;
    }

    public void ResetPlatform()
    {
        body.velocity = Vector3.zero;
        body.useGravity = false;
        transform.position = startPos;
        HasSymbol = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = gameObject.transform;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
