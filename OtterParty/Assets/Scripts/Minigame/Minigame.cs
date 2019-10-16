using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame : MonoBehaviour
{
    public int ID { get; set; }
    [SerializeField]
    private string minigameName;
    [SerializeField]
    private GameModes gamemode;
    [SerializeField]
    private int mingameDuration;
    [SerializeField]
    [Range(1,10.0f)]
    private int endOfMatchDelay;
    [SerializeField]
    private GameObject tutorialUI;

    private Dictionary<Player,bool> playersAlive;
    private int currentPoints = 1;
    private enum GameModes { FFA, AllvsOne, Team };

    private void Start()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playersAlive.Add(item,true);
        }
    }
    public bool IsLastPlayerStanding()
    {
        int temp = 0;
        foreach (var item in playersAlive)
        {
            temp = item.Value ? temp : temp++;
        }
        return temp == GameController.Instance.Players.Count - 1;
    }
    public void EliminatePlayer(Player p)
    {
        playersAlive[p] = false;
        //PointSystem.Instance.AwardPlayer(p,currentPoints);
        if (IsLastPlayerStanding())
        {
            GameIsOver();
        }
        currentPoints++;
    }
    public void ShowTutorialUI()
    {

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
        FreezeAll();
        ShowStandings();
        StartCoroutine("GoBackToOverviewScene");
    }
    IEnumerator GoBackToOverviewScene()
    {
        yield return new WaitForSeconds(endOfMatchDelay);
        SceneManager.LoadScene(Scenes.Instance.OverviewScene);
    }
    private void FreezeAll()
    {
        throw new NotImplementedException();
    }
    private void ShowStandings()
    {
        throw new NotImplementedException();
    }
}
