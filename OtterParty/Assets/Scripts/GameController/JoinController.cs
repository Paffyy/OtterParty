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
    [SerializeField]
    private ReadyUpUI readyUpUI;

    private List<GameObject> playerIndicators;
    private int playerCount = 0;
    private PlayerInputManager playerInputManager;
    private bool hasStarted;

    void Awake()
    {
        playerIndicators = new List<GameObject>();
        playerInputManager = GetComponent<PlayerInputManager>();
        foreach (Transform item in textParent.transform)
        {
            playerIndicators.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }
    private void OnPlayerJoined(PlayerInput input)
    {
        if (GameController.Instance != null)
        {
            Player p = new Player((int)input.playerIndex, "Player_" + input.playerIndex, input.devices[0], GameController.Instance.PlayerMaterials[input.playerIndex]);
            GameController.Instance.Players.Add(p);
            input.gameObject.GetComponent<MeshRenderer>().material = GameController.Instance.PlayerMaterials[input.playerIndex];
            playerIndicators[input.playerIndex].SetActive(true);
            playerCount++;
            readyUpUI.PlayerJoined(p);
            if (playerCount == 2)
            {
                readyUpUI.gameObject.SetActive(true);
                //StartCoroutine("StartDelay");
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
        yield return new WaitForSeconds(5f);
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
