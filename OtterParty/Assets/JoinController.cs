using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private GameObject textParent;
    private List<GameObject> texts;
    void Awake()
    {
        texts = new List<GameObject>();
        playerInputManager = GetComponent<PlayerInputManager>();
        foreach (Transform item in textParent.transform)
        {
            texts.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }
    private void OnPlayerJoined(PlayerInput input)
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.Players.Add(new Player((int)input.user.id, "Player_" + input.user.id, input.devices[0]));
            texts[input.playerIndex].SetActive(true);
        }
    }
}
