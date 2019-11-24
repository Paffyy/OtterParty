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

    [Header("Values")]

    [SerializeField]
    private int mingameDuration;
    [SerializeField]
    [Range(1,10.0f)]
    private int endOfMatchDelay;
    [SerializeField]
    [Range(1, 5f)]
    private int countDownTimer;
    [SerializeField]
    private GameType gameType;
    [SerializeField]
    [Range(20, 30f)]
    private float leadMultiplier;
    [SerializeField]
    [Range(1, 5)]
    private int miniGameLives;

    [Header("References")]
    [SerializeField]
    private GameObject countDownUI;
    [SerializeField]
    private GameObject showStandingsUI;
    [SerializeField]
    private GameObject winnerUI;
    [SerializeField]
    private GameObject miniGameUI;
    [SerializeField]
    private Animator countDownAnim;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject countDownTimerUI;
    [SerializeField]
    private GameObject timeLeftText;
    [SerializeField]
    private bool isOnUILayer;
    [SerializeField]
    private bool hasLimitedLives;
    public bool HasLimitedLives { get { return hasLimitedLives; } }
    public int MiniGameLives { get { return miniGameLives; } }
    public GameObject MiniGameUI { get { return miniGameUI; } }

    private GameModes gamemode;
    private RigidbodyConstraints playerConstraints;
    private Dictionary<Player,bool> playersAlive = new Dictionary<Player, bool>();
    private List<Transform> checkPoints = new List<Transform>();
    private Canvas canvas;

    private PlayerInputManager playerInputManager;
    private int currentPoints = 1;
    private int currentReversePoints = 1;
    private enum GameModes { FFA, AllvsOne, Team, Points };
    private enum GameType { LastManStanding, FirstToGoal, BothLastAndFirst, Finale, PointsBased };

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
        if (!isOnUILayer)
        {
            playerConstraints = playerPrefab.GetComponentInChildren<Rigidbody>().constraints;
        }
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
            float leadDistance = ((playerScore - (GameController.Instance.Minigames.Count - 1)) / (GameController.Instance.Players.Count * (GameController.Instance.Minigames.Count - 1))) * leadMultiplier;
            leadDistance = Mathf.Clamp(leadDistance, 0, leadMultiplier); // leadMultiplier is distance from checkpoint to end of running segment or less
            Debug.Log(leadDistance);
            Vector3 leadVector = new Vector3(0, 0, leadDistance);
            checkPoints[playerInput.playerIndex].transform.position = checkPoints[playerInput.playerIndex].transform.position + leadVector;
        }
        playerInput.gameObject.transform.position = checkPoints[playerInput.playerIndex].transform.position;
        playerInput.gameObject.transform.rotation = checkPoints[playerInput.playerIndex].transform.rotation;
        player.PlayerObject = playerInput.gameObject;

        if (!isOnUILayer) // ChickenShootout
        {
            Material[] mats = new Material[] { GameController.Instance.PlayerMaterials[player.ID] };
            player.PlayerObject.GetComponent<PlayerController>().MeshRenderer.materials = mats;
            var hat = GameController.Instance.PlayerHats[player.HatIndex];
            var hatTransform = playerInput.GetComponent<PlayerController>().HatPlaceHolder;
            var hatClone = Instantiate(hat, hatTransform.position + hat.GetComponent<PlayerHat>().HatOffset, hat.transform.rotation, hatTransform);
           // hat.GetComponent<PlayerHat>().SetPlayerMaterial(player.ID);
        }
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
                    EventHandler.Instance.Register(EventHandler.EventType.MultipleEliminateEvent, EliminateMultiplePlayers);
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

    private void EliminateMultiplePlayers(BaseEventInfo e)
    {
        var eventInfo = e as MultipleEliminateEventInfo;
        List<GameObject> playersToEliminate = eventInfo.EliminatedPlayers;
        foreach (var player in playersToEliminate)
        {
            Player p = GameController.Instance?.FindPlayerByGameObject(player);
            playersAlive[p] = false;
            UpdatePointSystem(p, currentPoints);
            player.SetActive(false);
        }
        currentPoints++;
        if (IsGameOver(0) )
        {
            GameIsOver();
        }
        else if(IsGameOver())
        {
            AwardLastPlayerAlive(true);
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
                {
                    UpdateAscendingPoints(p);
                    if (IsGameOver())
                        AwardLastPlayerAlive();
                }
                else
                {
                    UpdateReversePoints(p);
                    if (IsGameOver(0))
                        GameIsOver();
                }
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

    private void AwardLastPlayerAlive(bool reverseAdd = false)
    {
        Player lastPlayerAlive = playersAlive.FirstOrDefault(x => x.Value).Key;
        if (!reverseAdd)
        {
            UpdateAscendingPoints(lastPlayerAlive);
        }
        else
        {
            UpdateReversePoints(lastPlayerAlive);
        }
        playersAlive[lastPlayerAlive] = false;
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
        if(miniGameUI != null)
        {
           Instantiate(miniGameUI, canvas.transform);
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
            timeLeftText.SetActive(true);
            countDownTimerUI.SetActive(true);
            countDownTimerUI.GetComponent<CountDownTimer>().InitiateTimer(mingameDuration);
        }
    }

    IEnumerator MinigameTimer(int duration)
    {   
        yield return new WaitForSeconds(duration);
        if (gameType != GameType.PointsBased)
            AwardLastStandingPlayers();
        GameIsOver();
    }

    public void GameIsOver() 
    {
        EndMinigameMechanics();
        StopAllCoroutines();
        ToggleActive(false);
        if (gameType == GameType.PointsBased)
        {
            StartCoroutine("DisplayPlayerScores");
        }
        else
        {
            ShowStandingsUI();
            StartCoroutine("GoToNextScene");
        }
    }

    private IEnumerator DisplayPlayerScores()
    {
        ShowPlayerScores();
        yield return new WaitForSeconds(3);
        ConvertMinigamePointsToFinalePoints();
        ShowStandingsUI();
        StartCoroutine("GoToNextScene");
    }

    private void AwardLastStandingPlayers()
    {
        foreach (var item in playersAlive)
        {
            if (item.Value)
            {
                UpdatePointSystem(item.Key, currentPoints);
            }
        }
    }

    private void ConvertMinigamePointsToFinalePoints()
    {
        var sorted = from playerScore
                     in MinigamePointSystem.GetCurrentScore()
                     orderby -playerScore.Value
                     select playerScore;
        int placementOrder = GameController.Instance.Players.Count;
        foreach (var item in sorted.ToList())
        {
            MinigamePointSystem.GetCurrentScore()[item.Key] = placementOrder;
            placementOrder--;
        }
    }

    private void ShowPlayerScores()
    {
        var p = Instantiate(winnerUI, canvas.transform);
        p.GetComponent<AddPointsToPlayer>().IsPointsBased = true;
        Destroy(p, 3);
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
        if (!isOnUILayer)
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
   
    }
    private void ShowStandingsUI()
    {
        if (gameType == GameType.Finale)
            winnerUI.SetActive(true);
        else
        {
           Instantiate(showStandingsUI, canvas.transform);
        }
    }
}
