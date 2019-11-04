using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerIconSplitscreen : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField]
    private List<Image> playerImages;
    [SerializeField]
    private List<TextMeshProUGUI> playerScoreTexts;

    public List<TextMeshProUGUI> PlayerScoreTexts
    {
        get { return playerScoreTexts; }
        set { playerScoreTexts = value; }
    }

    private void Awake()
    {
        foreach (var item in playerImages)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        StartPointsUI();
        EventHandler.Instance.Register(EventHandler.EventType.UpdateScoreEvent, UpdatePlayerScore);

    }

    private void UpdatePlayerScore(BaseEventInfo e)
    {
        UpdatePlayerScoreEventInfo eventInfo = e as UpdatePlayerScoreEventInfo;
        if (eventInfo != null)
        {
            Player p = GameController.Instance.FindPlayerByGameObject(eventInfo.Player);
            playerScoreTexts[p.ID].gameObject.SetActive(true);
            playerScoreTexts[p.ID].text = eventInfo.Score.ToString(); // same animation as pointsystem
        }
    }
    public void StartPointsUI()
    {
        if (GameController.Instance != null && MinigameController.Instance != null)
        {
            var players = GameController.Instance.Players;
            for (int i = 0; i < players.Count; i++)
            {
                playerImages[i].gameObject.SetActive(true);
            }
        }
    }
}

