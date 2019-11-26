using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinUI : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;

    private Image playerIconImage;
    private Canvas canvas;
    private void Awake()
    {
        playerIconImage = GetComponentInChildren<Image>();
    }
    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.FinaleWinEvent, UpdatePlayerWonIcon);
        gameObject.SetActive(false);
    }

    private void UpdatePlayerWonIcon(BaseEventInfo e)
    {
        EliminateEventInfo finishedEventInfo = e as EliminateEventInfo;
        if (finishedEventInfo != null)
        {
            if (GameController.Instance != null && MinigameController.Instance != null)
            {
                var playerID = GameController.Instance.FindPlayerByGameObject(finishedEventInfo.PlayerToEliminate).ID;
                playerIconImage.sprite = sprites[playerID];
                MinigameController.Instance.EndMinigame();
            }
        }
    }
}
