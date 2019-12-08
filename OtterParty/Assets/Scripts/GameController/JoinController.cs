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
    void Start()
    {
        if (EventHandler.Instance != null)
        {
            EventHandler.Instance.Register(EventHandler.EventType.TransitionEvent, Transition);
        }
    }
    private void OnPlayerJoined(PlayerInput input)
    {
        if (GameController.Instance != null)
        {
            Player p = new Player((int)input.playerIndex, "Player_" + input.playerIndex, input.devices[0], GameController.Instance.PlayerMaterials[input.playerIndex]);
            GameController.Instance.Players.Add(p);
            p.PlayerObject = input.gameObject;
            Material[] mats = new Material[] { GameController.Instance.PlayerMaterials[input.playerIndex] };
            input.gameObject.GetComponent<PlayerController>().MeshRenderer.materials = mats;
            playerIndicators[input.playerIndex].SetActive(true);
            playerCount++;
            readyUpUI.PlayerJoined(p);
            readyUpUI.gameObject.SetActive(true);
        }
    }

    private void EnableStartButton()
    {
        startButton.SetActive(true);
        startButton.GetComponent<Animator>().SetTrigger("Selected");
    }
    private void Transition(BaseEventInfo e)
    {
        StartCoroutine("StartGameWithDelay");
    }

    public void StartGame()
    {
        GameController.Instance.InitPointSystem();
        GameController.Instance.StartNextMinigame();
    }

    IEnumerator StartGameWithDelay()
    {
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f);
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
