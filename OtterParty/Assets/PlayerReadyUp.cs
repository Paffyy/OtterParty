using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReadyUp : MonoBehaviour
{
    private void OnReadyUp()
    {
        var id = GetComponent<PlayerInput>().playerIndex;
        ReadyUpEventInfo e = new ReadyUpEventInfo(id);
        EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
    }
}
