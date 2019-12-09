using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 5f)]
    private float trapDoorUpTime;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float trapDoorDownTime;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartMechanics);
        StartMechanics(new StartMinigameEventInfo()); //Debug testing
    }
    private void StartMechanics(BaseEventInfo e)
    {
        StartCoroutine("ToggleTrapDoor");
    }

    IEnumerator ToggleTrapDoor()
    {
        anim.SetTrigger("ActivateTrapDoor");
        yield return new WaitForSeconds(trapDoorDownTime);
        anim.SetTrigger("ResetTrapDoor");
        yield return new WaitForSeconds(trapDoorUpTime);
        StartCoroutine("ToggleTrapDoor");
    }
}
