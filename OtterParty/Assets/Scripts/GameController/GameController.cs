using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PointSystem PointSystem { get; set; } 
    public List<Player> Players { get; set; } = new List<Player>();
    private List<Minigame> minigames = new List<Minigame>();
    private Minigame currentMinigame;

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
    public void StartNextMinigame()
    {
        SceneManager.LoadScene(currentMinigame.Id);
    }

}

