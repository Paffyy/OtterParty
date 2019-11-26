using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolPlatform : MonoBehaviour
{
    private MeshRenderer mesh;
    private SpriteRenderer spriteRen;
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
        spriteRen = GetComponentInChildren<SpriteRenderer>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetSymbol(Sprite sprite)
    {
        HasSymbol = true;
        spriteRen.sprite = sprite;
    }

    public void FallDown()
    {
        body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        body.useGravity = true;
        body.velocity += Vector3.down * fallSpeed;
    }

    public void ResetPlatform()
    {
        body.constraints = RigidbodyConstraints.FreezeAll;
        body.velocity = Vector3.zero;
        body.useGravity = false;
        transform.position = startPos;
        HasSymbol = false;
    }
}
