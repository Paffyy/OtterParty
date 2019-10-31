using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private GameObject textParent;
    private List<GameObject> texts;
    private int playerCount = 0;
    [SerializeField]
    private GameObject startButton;
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
            GameController.Instance.Players.Add(new Player((int)input.playerIndex, "Player_" + input.playerIndex, input.devices[0],  GameController.Instance.PlayerMaterials[input.playerIndex]));
            input.gameObject.GetComponent<MeshRenderer>().material = GameController.Instance.PlayerMaterials[input.playerIndex];
            texts[input.playerIndex].SetActive(true);
            playerCount++;
            if (playerCount == 2)
            {
                EnableStartButton();
            }
        }
    }

    private void EnableStartButton()
    {
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        GameController.Instance.InitPointSystem();
        GameController.Instance.StartNextMinigame();
    }
    private void Update()
    {

    }
}
