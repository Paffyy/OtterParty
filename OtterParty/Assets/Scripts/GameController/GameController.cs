using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PointSystem PointSystem { get; set; } = new PointSystem();
    public List<Player> Players { get; set; } = new List<Player>();
    public List<Minigame> Minigames { get; set; } = new List<Minigame>();
    private Minigame nextMinigame;
    private int nextMinigameIndex = 0;
    [SerializeField]
    private List<Material> playerMaterials;
    [SerializeField]
    private List<GameObject> playerHats;
    [SerializeField]
    private List<GameObject> playerDeathEffects;
    public List<GameObject> PlayerHats { get { return playerHats; } set { playerHats = value; } }
    public List<GameObject> PlayerDeathEffects { get { return playerDeathEffects; } set { playerDeathEffects = value; } }

    public List<Material> PlayerMaterials
    {
        get { return playerMaterials; }
        set { playerMaterials = value; }
    }

    #region Singleton
    private GameController() { }
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameController>();
            return instance;
        }
    }


    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        var parrent = GameObject.Find("Minigames");
        if (parrent != null)
        {
            foreach (Transform item in parrent.transform)
            {
                var minigame = item.gameObject.GetComponent<Minigame>();
                if (Minigames != null && item.gameObject.activeInHierarchy)
                {
                    Minigames.Add(minigame);
                }
            }
        }
        foreach (var item in playerHats)
        {
            item.GetComponent<PlayerHat>().IsAvailable = true;
        }
    }
    public void StartNextMinigame()
    {
        if (nextMinigameIndex < Minigames.Count)
        {
            nextMinigame = Minigames[nextMinigameIndex];
            SceneManager.LoadScene(nextMinigame.SceneIndex);
        }
        else
        {
            RestartGame();
        }
        nextMinigameIndex++;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        Destroy(ImportManager.Instance.gameObject);
        return;
    }

    public void InitPointSystem() 
    {
        PointSystem.InitializePlayers(Players);
    }
    public Player FindPlayerByGameObject(GameObject playerObject) =>  Players.FirstOrDefault(x => x.PlayerObject == playerObject);
    public Player FindPlayerByID(int playerIndex) => Players.FirstOrDefault(x => x.ID == playerIndex);

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            RestartGame();
        }
    }
}