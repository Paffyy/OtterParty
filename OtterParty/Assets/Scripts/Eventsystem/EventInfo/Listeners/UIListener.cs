using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListener : MonoBehaviour
{
    private Dictionary<int, PlayerLives> playerLives = new Dictionary<int, PlayerLives>();
    [SerializeField]
    private HitListener hitListener;
    private int numberOfLives;
    private PlayerLives playerLivesPrefab;
    [SerializeField]
    private List<Sprite> playerSprites;
    [SerializeField]
    private List<Sprite> playerHearts;
    [SerializeField]
    private int xOffset;
    [SerializeField]
    private List<Transform> spritePositions;
    

    void Start()
    {
        numberOfLives = hitListener.NumberOfLives;
        foreach (var item in GameController.Instance.Players)
        {
            PlayerLives pl = Instantiate(playerLivesPrefab, transform);
            pl.player = item;
            pl.PlayerImage.sprite = playerSprites[item.ID];
            pl.PlayerHeart.sprite = playerHearts[item.ID];
        }
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, UpdateLives);
    }

    private void UpdateLives(BaseEventInfo e)
    {

    }
}
