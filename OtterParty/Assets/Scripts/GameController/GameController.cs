using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //public PointSystem PointSystem { get; set; } 
    private Minigame currentMinigame;
    public List<Player> Players { get; set; }
    private List<Minigame> minigames;

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

    private void Start()
    {
        currentMinigame = minigames[0];
        StartNextMinigame();
    }
    public void StartNextMinigame()
    {
        // load minigame
        SceneManager.LoadScene(currentMinigame.ID);
    }

}
public class Player // debug
{

}
