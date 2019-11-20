using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrosshairSpawned : MonoBehaviour
{
    [SerializeField]
    private Transform playerParent;
    private Canvas canvas;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    void OnPlayerJoined(PlayerInput input)
    {
        input.gameObject.transform.SetParent(playerParent);
        input.gameObject.transform.position = Vector3.zero;
    }
}
