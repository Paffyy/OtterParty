using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinController : MonoBehaviour
{
    [SerializeField]
    private GameObject textParent;
    [SerializeField]
    private int joinDuration;
    [SerializeField]
    private GameObject startButton;

    private List<GameObject> texts;
    private int playerCount = 0;
    private PlayerInputManager playerInputManager;
    private bool hasStarted;

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
                StartCoroutine("StartDelay");
            }
        }
    }

    private void EnableStartButton()
    {
        startButton.SetActive(true);
        startButton.GetComponent<Animator>().SetTrigger("Selected");
    }

    public void StartGame()
    {
        GameController.Instance.InitPointSystem();
        GameController.Instance.StartNextMinigame();
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3);
        EnableStartButton();
    }

    //IEnumerator StartingIn()
    //{
    //    yield return new WaitForSeconds(joinDuration);
    //    GameController.Instance.InitPointSystem();
    //    GameController.Instance.StartNextMinigame();
    //}
    private void Update()
    {

    }
}
