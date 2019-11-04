using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    #region Fields
    private GameModes gamemode;
    [SerializeField]
    private int mingameDuration;
    [SerializeField]
    [Range(1,10.0f)]
    private int endOfMatchDelay;
    [SerializeField]
    [Range(1, 5f)]
    private int countDownTimer;
    [SerializeField]
    private GameObject countDownUI;
    [SerializeField]
    private GameObject showStandingsUI;
    [SerializeField]
    private GameObject winnerUI;
    [SerializeField]
    private GameObject splitscreenUI;
    [SerializeField]
    private Animator countDownAnim;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameType gameType;
    [SerializeField]
    private float leadMultiplier;
    [SerializeField]
    private GameObject countDownTimerUI;

    private RigidbodyConstraints playerConstraints;
    private Dictionary<Player,bool> playersAlive = new Dictionary<Player, bool>();
    private List<Transform> checkPoints = new List<Transform>();
    private Canvas canvas;

    private PlayerInputManager playerInputManager;
    private int currentPoints = 1;
    private int currentReversePoints = 1;
    private enum GameModes { FFA, AllvsOne, Team, Points };
    private enum GameType { LastManStanding, FirstToGoal, BothLastAndFirst, Finale };

    #endregion

    public PointSystem MinigamePointSystem { get; set; } = new PointSystem();

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

    #region Init
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerPrefab;
        playerConstraints = playerPrefab.GetComponentInChildren<Rigidbody>().constraints;
        foreach (Transform item in gameObject.transform)
        {
            checkPoints.Add(item);
        }
    }
    private void Start()
    {
        if (GameController.Instance != null)
        {
            InitPlayers();
            RegisterToEliminateEvents();
            currentReversePoints = GameController.Instance.Players.Count;
        }
    }
    private void InitPlayers()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playersAlive.Add(item, true);
        }
        MinigamePointSystem.InitializePlayers(GameController.Instance.Players);
    }
    public void JoinPlayers()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playerInputManager.JoinPlayer(item.ID, -1, null, item.Device);
        }
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Player player = GameController.Instance.FindPlayerByID(playerInput.playerIndex);
        if (gameType == GameType.Finale)
        {
            float playerScore = GameController.Instance.PointSystem.GetCurrentScore().FirstOrDefault(x => x.Key == player).Value;
            Vector3 leadVector = new Vector3(0, 0, playerScore / (GameController.Instance.Players.Count * GameController.Instance.Minigames.Count) * leadMultiplier);
            checkPoints[playerInput.playerIndex].transform.position = checkPoints[playerInput.playerIndex].transform.position + leadVector;
        }
        playerInput.gameObject.transform.position = checkPoints[playerInput.playerIndex].transform.position;
        playerInput.gameObject.transform.rotation = checkPoints[playerInput.playerIndex].transform.rotation;
        player.PlayerObject = playerInput.gameObject;
        player.PlayerObject.GetComponent<MeshRenderer>().material = GameController.Instance.PlayerMaterials[player.ID];
    }
    #endregion

    #region Events
    private void RegisterToEliminateEvents()
    {
        if (EventHandler.Instance != null)
        {
            switch (gameType)
            {
                case GameType.LastManStanding:
                    EventHandler.Instance.Register(EventHandler.EventType.EliminateEvent, EliminatePlayerEvent);
                    break;
                case GameType.FirstToGoal:
                    EventHandler.Instance.Register(EventHandler.EventType.FinishLineEvent, GiveScoreEvent);
                    break;
                case GameType.BothLastAndFirst:
                    EventHandler.Instance.Register(EventHandler.EventType.EliminateEvent, EliminatePlayerEvent);
                    EventHandler.Instance.Register(EventHandler.EventType.FinishLineEvent, GiveScoreEvent);
                    break;
                default:
                    break;
            }
        }
    }

    private void EndMinigameMechanics()
    {
        EventHandler.Instance.FireEvent(EventHandler.EventType.EndMinigameEvent, new EndMinigameEventInfo());
    }

    private void StartMinigameMechanics()
    {
        EventHandler.Instance.FireEvent(EventHandler.EventType.StartMinigameEvent, new StartMinigameEventInfo());
    }
    private void EliminatePlayerEvent(BaseEventInfo e)
    {
        var eliminateEventInfo = e as EliminateEventInfo;
        if (eliminateEventInfo != null)
        {
            var player = GameController.Instance?.FindPlayerByGameObject(eliminateEventInfo.PlayerToEliminate);
            if (player != null)
            {
                EliminatePlayer(player,true);
                eliminateEventInfo.PlayerToEliminate.SetActive(false);
            }
        }
    }
    private void GiveScoreEvent(BaseEventInfo e)
    {
        var finishEventInfo = e as FinishedEventInfo;
        if (finishEventInfo != null)
        {
            var player = GameController.Instance?.FindPlayerByGameObject(finishEventInfo.PlayerWhoFinished);
            if (player != null)
            {
                currentPoints = GameController.Instance.Players.Count;
                EliminatePlayer(player, false);
                finishEventInfo.PlayerWhoFinished.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
    #endregion
 
    public void EliminatePlayer(Player p, bool playerWasEliminated) // FFA
    {
        if (!playersAlive[p])
        {
            return;
        }
        playersAlive[p] = false;
        switch (gameType)
        {
            case GameType.LastManStanding:
            {
                UpdateAscendingPoints(p);
                if (IsGameOver())
                     AwardLastPlayerAlive();
                break;
            }
            case GameType.FirstToGoal:
            {
                UpdateReversePoints(p);
                if (IsGameOver(0))
                    GameIsOver();
                break;
            }
            case GameType.BothLastAndFirst:
            {
                if (playerWasEliminated)
                    UpdateAscendingPoints(p);
                else
                    UpdateReversePoints(p);
                if (IsGameOver())
                    AwardLastPlayerAlive();
                break;
            }
            default:
                break;
        }
    }

    private void UpdateAscendingPoints(Player p)
    {
        UpdatePointSystem(p, currentPoints);
        currentPoints++;
    }

    private void UpdateReversePoints(Player p)
    {
        UpdatePointSystem(p, currentReversePoints);
        currentReversePoints--;
    }

    private void AwardLastPlayerAlive()
    {
        Player lastPlayerAlive = playersAlive.FirstOrDefault(x => x.Value).Key;
        UpdatePointSystem(lastPlayerAlive, currentPoints);
        GameIsOver();
    }

    private void UpdatePointSystem(Player p, int points)
    {
        var playerPoints = new Dictionary<Player, int>();
        playerPoints.Add(p, points);
        MinigamePointSystem.UpdateScore(playerPoints);
    }

    public void StartMinigame()
    {
        StartCoroutine("StartCountDown");
        ToggleActive(false);
    }
    IEnumerator StartCountDown() // TODO Display The CountDown UI
    {
        countDownAnim.SetBool("IsCountingDown", true);
        if (gameType == GameType.Finale)
        {
            Instantiate(splitscreenUI, canvas.transform);
        }
        yield return new WaitForSeconds(countDownTimer);
        ToggleActive(true);
        StartMinigameTimer();
        StartMinigameMechanics();
    }
 
    public void StartMinigameTimer()
    {
        StartCoroutine("MinigameTimer", mingameDuration);
        if(countDownTimerUI != null)
        {
            countDownTimerUI.SetActive(true);
            countDownTimerUI.GetComponent<CountDownTimer>().InitiateTimer(mingameDuration);
        }
    }

    IEnumerator MinigameTimer(int duration)
    {   
        yield return new WaitForSeconds(duration);
        GameIsOver();
    }

    public void GameIsOver() 
    {
        EndMinigameMechanics();
        StopAllCoroutines();
        ToggleActive(false);
        ShowStandingsUI();
        StartCoroutine("GoToNextScene");
    }

    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(endOfMatchDelay);
        GameController.Instance.StartNextMinigame();
    }

    private bool IsGameOver(int playerCountOffset = 1) // FFA
    {
        int temp = 0;
        foreach (var item in playersAlive)
        {
            temp = item.Value ? temp : temp + 1;
        }
        return temp == playersAlive.Count - playerCountOffset;
    }

    private void ToggleActive(bool toggle)
    {
        foreach (var item in GameController.Instance.Players)
        {
            if (toggle)
            {
                item.PlayerObject.GetComponent<PlayerController>().IsActive = true;
                item.PlayerObject.GetComponent<Rigidbody>().constraints = playerConstraints;
            }
            else
            {
                item.PlayerObject.GetComponent<PlayerController>().IsActive = false;
                item.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void ShowStandingsUI()
    {
        if (gameType == GameType.Finale)
        {
            winnerUI.SetActive(true);
        }
        else
        {
            Instantiate(showStandingsUI, canvas.transform);
        }
    }
}
