using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //public PointSystem PointSystem { get; set; } 
    public List<Player> Players { get; set; }
    private List<Minigame> minigames;
    private Minigame currentMinigame;

    #region Singleton
    private GameController() { }
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
                instance = new GameController();
            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        var parrent = GameObject.Find("Minigames");
        if (parrent != null)
        {
            foreach (GameObject item in parrent.transform)
            {
                var minigame = item.GetComponent<Minigame>();
                if (minigame != null)
                {
                    minigames.Add(minigame);
                }
            }
            currentMinigame = minigames[0];
        }
    }
    private void Start()
    {
        StartNextMinigame();
    }
    public void StartNextMinigame()
    {
        // load minigame
        SceneManager.LoadScene(currentMinigame.Id);
    }

}
public class Player // debug
{

}
