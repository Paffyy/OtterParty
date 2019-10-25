using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField]
    void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.Players.Add(new Player((int)input.user.id,"Player_" + input.user.id, input.devices[0]));
        }
    }
}
