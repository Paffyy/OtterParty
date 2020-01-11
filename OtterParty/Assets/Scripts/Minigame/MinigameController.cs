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

    [Header("Debugging")]
    [SerializeField]
    private bool toggleDebugging;

    [Header("Values")]

    [SerializeField]
    private int minigameDuration;
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
    [SerializeField]
    private bool isOnUILayer;
    [SerializeField]
    private bool hasLimitedLives;

    [Header("References")]
    [SerializeField]
    private AudioClip countDownSound;
    [SerializeField]
    private AudioClip applauseSound;
    [SerializeField]
    private float applauseSoundVolume;
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
    private ReadyUpUI readyUpUI;
    [SerializeField]
    private int didNotFinishTimer;
    public GameType CurrentGameType { get { return gameType; } }
    public bool HasLimitedLives { get { return hasLimitedLives; } }
    public int MiniGameLives { get { return miniGameLives; } }
    public GameObject MiniGameUI { get { return miniGameUI; } }

    private GameModes gamemode;
    private RigidbodyConstraints playerConstraints;
    private Dictionary<Player,bool> playersAlive = new Dictionary<Player, bool>();
    private List<Transform> spawnPoints = new List<Transform>();
    private Canvas canvas;
    private PlayerInputManager playerInputManager;
    private int currentPoints = 1;
    private int currentReversePoints = 1;
    private enum GameModes { FFA, AllvsOne, Team, Points };
    public enum GameType { LastManStanding, FirstToGoal, BothLastAndFirst, Finale, PointsBased, PointsAndLives };

    public List<float> PlayerPercentageScore { get; set; } = new List<float>();
    public PointSystem MinigamePointSystem { get; set; } = new PointSystem();
    
    #endregion

    #region Singleton Implementation
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
            spawnPoints.Add(item);
        }
    }
    private void Start()
    {
        if (GameController.Instance != null)
        {
            InitPlayers();
            RegisterToEliminateEvents();
            currentReversePoints = GameController.Instance.Players.Count;
            toggleDebugging = false;
        }
        else
        {
            if (toggleDebugging)
            {
                EnableTesting();
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
    public void JoinPlayers()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playerInputManager.JoinPlayer(item.ID, -1, null, item.Device);
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if (!toggleDebugging)
        {
            Player player = GameController.Instance.FindPlayerByID(playerInput.playerIndex);
            player.PlayerObject = playerInput.gameObject;
            if (!isOnUILayer) // Exception where the character has no mesh
            {
                Material[] mats = new Material[] { GameController.Instance.PlayerMaterials[player.ID] };
                player.PlayerObject.GetComponent<PlayerController>().MeshRenderer.materials = mats;
                var hat = GameController.Instance.PlayerHats[player.HatIndex].GetComponent<PlayerHat>();
                var hatTransform = playerInput.GetComponent<PlayerController>().HatPlaceHolder;
                var hatClone = Instantiate(hat.gameObject, hatTransform.position, hat.transform.rotation, hatTransform);
                hatClone.transform.localPosition = hat.HatOffset;
                hatClone.transform.localEulerAngles = hat.HatRotation;
                hatClone.GetComponent<PlayerHat>().SetHatMaterial(player.ID);
            }
            readyUpUI.PlayerJoined(player);
            readyUpUI.gameObject.SetActive(true);
        }
        playerInput.gameObject.transform.position = spawnPoints[playerInput.playerIndex].transform.position;
        playerInput.gameObject.transform.rotation = spawnPoints[playerInput.playerIndex].transform.rotation;
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
                case GameType.Finale:
                    EventHandler.Instance.Register(EventHandler.EventType.FinishLineEvent, PlayerHasMadeItToGoal);
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

    private void EliminateMultiplePlayers(BaseEventInfo e)
    {
        var eventInfo = e as MultipleEliminateEventInfo;
        List<GameObject> playersToEliminate = eventInfo.EliminatedPlayers;
        foreach (var player in playersToEliminate)
        {
            Player p = GameController.Instance?.FindPlayerByGameObject(player);
            playersAlive[p] = false;
            UpdatePointSystem(p, currentPoints);
            var deathEffect = Instantiate(GameController.Instance.PlayerDeathEffects[p.ID], p.PlayerObject.transform.position, p.PlayerObject.transform.rotation);
            Destroy(deathEffect.gameObject, 2);
            player.SetActive(false);
        }
        currentPoints++;
        if (IsGameOver(0))
        {
            EndMinigame();
        }
        else if(IsGameOver())
        {
            AwardLastPlayerAlive(true);
        }
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
                var deathEffect = Instantiate(GameController.Instance.PlayerDeathEffects[player.ID], player.PlayerObject.transform.position, player.PlayerObject.transform.rotation);
                Destroy(deathEffect.gameObject, 1);
                player.PlayerObject.SetActive(false);
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

    private void PlayerHasMadeItToGoal(BaseEventInfo e)
    {
        if (!timeLeftText.activeSelf)
        {
            StartDNFTimer();
        }
        FinishedEventInfo fei = e as FinishedEventInfo;
        var player = GameController.Instance.FindPlayerByGameObject(fei.PlayerWhoFinished);
        playersAlive[player] = false;
        UpdateReversePoints(player);
        player.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (IsGameOver(0))
        {
            EndMinigame();
        }
    }
    #endregion

    public void EliminatePlayer(Player p, bool playerWasEliminated)
    {
        if (!playersAlive[p])
            return;
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
                    EndMinigame();
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
                        EndMinigame();
                }
                break;
            }
            case GameType.PointsAndLives:
                {
                    if (IsGameOver(0))
                        EndMinigame();
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
        EndMinigame();
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
    IEnumerator StartCountDown()
    {
        countDownAnim.SetBool("IsCountingDown", true);
        SoundEventInfo sei = new SoundEventInfo(countDownSound, 0, 2);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
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
        StartCoroutine("MinigameTimer", minigameDuration);
        if(countDownTimerUI != null && CurrentGameType != GameType.Finale)
        {
            timeLeftText.SetActive(true);
            countDownTimerUI.SetActive(true);
            countDownTimerUI.GetComponent<CountDownTimer>().InitiateTimer(minigameDuration);
        }
    }

    public void StartDNFTimer()
    {
        StopCoroutine("MinigameTimer");
        StartCoroutine("MinigameTimer", didNotFinishTimer);
        if (countDownTimerUI != null)
        {
            timeLeftText.SetActive(true);
            countDownTimerUI.SetActive(true);
            countDownTimerUI.GetComponent<CountDownTimer>().InitiateTimer(didNotFinishTimer);
        }

    }
    IEnumerator DNFTimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        EndMinigame();
    }
    IEnumerator MinigameTimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        if (gameType != GameType.PointsBased && gameType != GameType.PointsAndLives)
        {
            AwardLastStandingPlayers();
        }
        EndMinigame();
    }

    public void EndMinigame()
    {
        EndMinigameMechanics();
        StopAllCoroutines();
        ToggleActive(false);
        SoundEventInfo sei = new SoundEventInfo(applauseSound, applauseSoundVolume, 2);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        if (gameType == GameType.PointsBased || gameType == GameType.PointsAndLives)
        {
            StartCoroutine("DisplayPlayerScores");
        }
        else if (gameType == GameType.Finale)
        {
            var playerPoints = new Dictionary<Player, int>();
            foreach (var item in MinigamePointSystem.GetCurrentScore())
            {
                playerPoints.Add(item.Key, (int) (item.Value * ImportManager.Instance.Settings.FinaleScoreMultiplier));
            }
            MinigamePointSystem.UpdateScore(playerPoints);
            ShowStandingsUI();
            StartCoroutine("GoToNextScene", endOfMatchDelay);
        }
        else
        {
            ShowStandingsUI();
            StartCoroutine("GoToNextScene", endOfMatchDelay);
        }
    }

    private IEnumerator DisplayPlayerScores()
    {
        ShowPlayerScores();
        yield return new WaitForSeconds(4);
        ConvertMinigamePointsToFinalePoints();
        ShowStandingsUI();
        StartCoroutine("GoToNextScene", endOfMatchDelay);
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
                     orderby playerScore.Value
                     select playerScore;
        int placementOrder = 1;
        KeyValuePair<Player,int> previousScore = new KeyValuePair<Player, int>(new Player(),int.MinValue);
        foreach (var item in sorted.ToList())
        {
            if (item.Value == previousScore.Value)
            {
                MinigamePointSystem.GetCurrentScore()[item.Key] = MinigamePointSystem.GetCurrentScore()[previousScore.Key];
            }
            else
            {
                MinigamePointSystem.GetCurrentScore()[item.Key] = placementOrder;
            }
            placementOrder++;
            previousScore = item;
        }
    }

    private void ShowPlayerScores()
    {
        var p = Instantiate(winnerUI, canvas.transform);
        p.GetComponent<UIPlayerScore>().IsPointsBased = true;
        Destroy(p, 4);
    }

    private IEnumerator GoToNextScene(float duration)
    {
        yield return new WaitForSeconds(duration);
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

    public void ToggleActive(bool toggle)
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
           Instantiate(showStandingsUI, canvas.transform);
    }
    private void EnableTesting()
    {
        canvas.gameObject.SetActive(false);
        playerInputManager.JoinPlayer();
        FindObjectOfType<PlayerController>().IsActive = true;
        EventHandler.Instance.FireEvent(EventHandler.EventType.StartMinigameEvent, new StartMinigameEventInfo());
    }
}