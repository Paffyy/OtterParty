using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerController : MonoBehaviour
{

    private PlayerInputManager playerInputManager;
    [SerializeField]
    private List<Transform> checkPoints;
    private bool joined;
    void Awake()
    {
        checkPoints = new List<Transform>();
        playerInputManager = GetComponent<PlayerInputManager>();
        foreach (Transform item in gameObject.transform)
        {
            checkPoints.Add(item);
        }
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        input.gameObject.transform.position = checkPoints[input.playerIndex].transform.position;
        input.gameObject.transform.rotation = checkPoints[input.playerIndex].transform.rotation;
        input.gameObject.GetComponent<MeshRenderer>().material.color = PlayerColors.Instance.Colors[input.playerIndex];
    }
    private void MovePimToCheckpoint(uint id)
    {
        playerInputManager.transform.position = checkPoints[(int)id].transform.position;
        playerInputManager.transform.rotation = checkPoints[(int)id].transform.rotation;

    }
}
