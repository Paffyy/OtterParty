﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private List<Material> playerMaterials;
    [SerializeField]
    private GameObject textParent;
    private List<GameObject> texts;
    void Awake()
    {
        texts = new List<GameObject>();
        playerInputManager = GetComponent<PlayerInputManager>();
        foreach (Transform item in textParent.transform)
        {
            texts.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }
    private void OnPlayerJoined(PlayerInput input)
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.Players.Add(new Player((int)input.playerIndex, "Player_" + input.playerIndex, input.devices[0], playerMaterials[input.playerIndex]));
            input.gameObject.GetComponent<MeshRenderer>().material = playerMaterials[input.playerIndex];
            texts[input.playerIndex].SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameController.Instance.StartNextMinigame();
        }
    }
}
