using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameController : MonoBehaviour
{
    private GameModes gamemode;
    [SerializeField]
    private int mingameDuration;
    [SerializeField]
    [Range(1,10.0f)]
    private int endOfMatchDelay;
    [SerializeField]
    [Range(1, 5f)]
    private int countDownTimer;

    private List<GameObject> playerObjects = new List<GameObject>();
    private Dictionary<Player,bool> playersAlive = new Dictionary<Player, bool>();
    private List<Transform> checkPoints = new List<Transform>();
    private PointSystem minigamePointSystem;

    private PlayerInputManager playerInputManager;
    private int currentPoints = 1;
    private enum GameModes { FFA, AllvsOne, Team, Points };

    #region Singleton
    private MinigameController() { }
    private static MinigameController instance;
    public static MinigameController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<MinigameController>();
            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        foreach (var item in GameController.Instance.Players)
        {
            playersAlive.Add(item,true);
        }
        foreach (Transform item in gameObject.transform)
        {
            checkPoints.Add(item);
        }
    }
    private void Start()
    {
        JoinPlayers();
    }

    public void EliminatePlayer(Player p) // FFA
    {
        playersAlive[p] = false;
        var playerPoints = new Dictionary<Player, int>();
        playerPoints.Add(p, currentPoints);
        minigamePointSystem.UpdateScore(playerPoints);
        currentPoints++;
        if (IsLastPlayerStanding())
        {
            GameIsOver(); 
        }
    }
    private bool IsLastPlayerStanding() // FFA
    {
        int temp = 0;
        foreach (var item in playersAlive)
        {
            temp = item.Value ? temp : temp++;
        }
        return temp == GameController.Instance.Players.Count - 1;
    }
    public void StartMinigame()
    {
        StartCoroutine("StartCountDown");
    }
    IEnumerator StartCountDown() // TODO Display The CountDown UI
    {
        yield return new WaitForSeconds(countDownTimer);
        StartMinigameTimer();
        EnablePlayers();
    }
    private void JoinPlayers()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playerInputManager.JoinPlayer(item.ID, -1, null, item.Device);
        }
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // asign player gameobjects here
        playerInput.gameObject.transform.position = checkPoints[playerInput.playerIndex].transform.position;
        playerInput.gameObject.transform.rotation = checkPoints[playerInput.playerIndex].transform.rotation;
        playerInput.gameObject.GetComponent<MeshRenderer>().material = GameController.Instance.PlayerMaterials[playerInput.playerIndex];
    }
    private void EnablePlayers()
    {
        foreach (var item in playerObjects)
        {
            // unlock players
        }
    }

    public void StartMinigameTimer(int duration = 60)
    {
        StartCoroutine("MinigameTimer", duration);
    }
    IEnumerator MinigameTimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        GameIsOver();
    }
    public void GameIsOver() 
    {
        //Adds point to the last guy and adds points to all remaining players if timer runs out
        var playerPoints = new Dictionary<Player, int>();
        foreach (var item in playersAlive.Where(x => x.Value)) 
        {
            playerPoints.Add(item.Key, currentPoints);
        }
        minigamePointSystem.UpdateScore(playerPoints);

        StopAllCoroutines();
        FreezeAll();
        ShowStandingsUI();
        if (GameController.Instance != null)
        {
            GameController.Instance.PointSystem.UpdateScore(minigamePointSystem.GetCurrentScore());
        }
        StartCoroutine("GoToNextScene");
    }
    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(endOfMatchDelay);
        GameController.Instance.StartNextMinigame();
    }
    private void FreezeAll()
    {
        throw new NotImplementedException();
    }
    private void ShowStandingsUI()
    {
        throw new NotImplementedException();
    }

    /* Tutorial UI -> Display Untill every1 clicks
     * CountDownTimer UI -> Show Timer when counting down
     * Show updates on points/eliminations during game
     * Show Scores adding to main score and prep next game
     */
}
