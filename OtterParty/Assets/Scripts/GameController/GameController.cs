using System.Collections;
using System.Collections.Generic;
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
                if (Minigames != null)
                {
                    Minigames.Add(minigame);
                }
            }
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
            SceneManager.LoadScene(0);
            nextMinigameIndex = 0;
            return;
        }
        nextMinigameIndex++;
    }
    public void InitPointSystem() // Should run after Players have joined
    {
        PointSystem.InitializePlayers(Players);
    }
}

