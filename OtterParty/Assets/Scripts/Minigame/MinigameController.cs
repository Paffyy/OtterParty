using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private Dictionary<Player,bool> playersAlive = new Dictionary<Player, bool>();
    private List<Transform> checkPoints = new List<Transform>();
    public PointSystem MinigamePointSystem { get; set; } = new PointSystem();
    private Canvas canvas;

    private PlayerInputManager playerInputManager;
    //TODO Factorize 
    private int currentPoints = 1;
    private int currentReversePoints = 1;
    private enum GameModes { FFA, AllvsOne, Team, Points };
    private enum GameType { LastManStanding, FirstToGoal, BothLastAndFirst, Finale };
    // this
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
        canvas = FindObjectOfType<Canvas>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerPrefab;
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

    private void EliminatePlayerEvent(BaseEventInfo e)
    {
        var eliminateEventInfo = e as EliminateEventInfo;
        if (eliminateEventInfo != null)
        {
            var player = GameController.Instance?.Players.FirstOrDefault(x => x.PlayerObject == eliminateEventInfo.PlayerToEliminate);
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
            var player = GameController.Instance?.Players.FirstOrDefault(x => x.PlayerObject == finishEventInfo.PlayerWhoFinished);
            if (player != null)
            {
                currentPoints = GameController.Instance.Players.Count;
                EliminatePlayer(player, false);
                finishEventInfo.PlayerWhoFinished.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
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
    public void EliminatePlayer(Player p, bool wasPlayerEliminated) // FFA
    {
        playersAlive[p] = false;
        switch (gameType)
        {
            case GameType.LastManStanding:
            {
                UpdatePointSystem(p,currentPoints);
                currentPoints++;
                if (IsGameOver())
                {
                    Player lastPlayerAlive = playersAlive.FirstOrDefault(x => x.Value).Key;
                    UpdatePointSystem(lastPlayerAlive,currentPoints);
                    GameIsOver();
                }
                break;
            }
            case GameType.FirstToGoal:
            {
                UpdatePointSystem(p, currentReversePoints);
                currentReversePoints--;
                if (IsGameOver(0))
                    GameIsOver();
                break;
            }
            case GameType.BothLastAndFirst:
            {
                if (wasPlayerEliminated)
                {
                    UpdatePointSystem(p, currentPoints);
                    currentPoints++;
                }
                else
                {
                    UpdatePointSystem(p, currentPoints);
                    currentReversePoints--;
                }
                if (IsGameOver(0))
                    GameIsOver();
                break;
            }
            default:
                break;
        }
    }

    private void UpdatePointSystem(Player p, int points)
    {
        var playerPoints = new Dictionary<Player, int>();
        playerPoints.Add(p, points);
        MinigamePointSystem.UpdateScore(playerPoints);
    }

    private bool IsGameOver(int playerCountOffset = 1) // FFA
    {
        int temp = 0;
        foreach (var item in playersAlive)
        {
            temp = item.Value ? temp : temp + 1;
        }
        Debug.Log(temp == GameController.Instance.Players.Count - playerCountOffset);
        return temp == GameController.Instance.Players.Count - playerCountOffset;
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
    private void StartMinigameMechanics()
    {
        EventHandler.Instance.FireEvent(EventHandler.EventType.StartMinigameEvent, new StartMinigameEventInfo());
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
        Player player = GameController.Instance.Players.FirstOrDefault(x => x.ID == playerInput.playerIndex);
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
    private void ToggleActive(bool toggle)
    {
        foreach (var item in GameController.Instance.Players)
        {
            if (toggle)
            {
                item.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                item.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
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
