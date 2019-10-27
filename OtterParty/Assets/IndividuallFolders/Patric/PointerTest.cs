using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PointerTest : MonoBehaviour
{
    [SerializeField]
    private int moveSpeed;
    private RectTransform rectTransform;
    private Vector3 movement;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {

        rectTransform.position += movement * moveSpeed;
    }
    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        movement = new Vector3(input.x,input.y);
        Debug.Log("test");
    }
    private void OnFire()
    {
        rectTransform.position += movement * moveSpeed * 10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
        }
    }
}
