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
        gameObject.SetActive(false);
        if (EventHandler.Instance != null)
        {
            //EventHandler.Instance.Register(EventHandler.EventType.FinaleWinEvent, ShowPlayerWinUI);
        }
    }

    private void ShowPlayerWinUI(BaseEventInfo e)
    {
        FinishedEventInfo finishedEventInfo = e as FinishedEventInfo;
        if (finishedEventInfo != null)
        {
            if (GameController.Instance != null && MinigameController.Instance != null)
            {
                var playerID = GameController.Instance.Players.FirstOrDefault(x => x.PlayerObject == finishedEventInfo.PlayerWhoFinished).ID;
                playerIconImage.sprite = sprites[playerID];
                gameObject.SetActive(true);
                MinigameController.Instance.GameIsOver();
            }
         
        }
    }
}
