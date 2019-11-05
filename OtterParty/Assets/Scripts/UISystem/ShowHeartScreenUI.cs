using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHeartScreenUI : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField]
    private List<Image> playerImages;
    [SerializeField]
    private List<Image> playerHearts;
    private Dictionary<int, List<GameObject>> hearts = new Dictionary<int, List<GameObject>>();

    private void Awake()
    {
        foreach (var item in playerImages)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (GameController.Instance != null && MinigameController.Instance != null)
        {
            InstantiateUI();
            EventHandler.Instance.Register(EventHandler.EventType.HitEvent, UpdatePlayerLives);
        }
    }

    private void InstantiateUI()
    {
        var players = GameController.Instance.Players;
        for (int i = 0; i < players.Count; i++)
        {
            int offset = 30;
            var heart = playerHearts[i].gameObject;
            heart.SetActive(true);
            playerImages[i].gameObject.SetActive(true);
            List<GameObject> currentPlayerHearts = new List<GameObject>();
            currentPlayerHearts.Add(heart);
            for (int x = 1; x < MinigameController.Instance.MiniGameLives; x++)
            {
                var life = Instantiate(heart, heart.transform.parent);
                var rectTransform = life.GetComponent<RectTransform>();
                if(i == 0 || i == 2)
                {
                    rectTransform.position = new Vector3(rectTransform.position.x + offset, rectTransform.position.y, rectTransform.position.z);
                }
                else if (i == 1 || i == 3)
                {
                    rectTransform.position = new Vector3(rectTransform.position.x - offset, rectTransform.position.y, rectTransform.position.z);
                }
                offset += offset;
                currentPlayerHearts.Add(life);
            }
            hearts.Add(i, currentPlayerHearts);
        }
    }

    private void UpdatePlayerLives(BaseEventInfo e)
    {
        Debug.Log("Updating player lives");
        var hitEventInfo = e as HitEventInfo;
        GameObject playerHit = hitEventInfo.ObjectHit;
        Player player = GameController.Instance.FindPlayerByGameObject(playerHit);
        List<GameObject> listOfHearts = hearts[player.ID];
        int count = listOfHearts.Count;
        if(count > 0)
        {
            listOfHearts[count - 1].SetActive(false);
            listOfHearts.RemoveAt(count - 1);
        }

    }

}
