using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 5f)]
    private float trapDoorResetTime;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float startDelay;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartMechanics);
       // StartMechanics(new StartMinigameEventInfo()); //Debug testing
    }
    private void StartMechanics(BaseEventInfo e)
    {

        StartCoroutine("ToggleTrapDoor");
    }

    IEnumerator ActivateTrapDoor()
    {
        anim.SetTrigger("ActivateTrapDoor");
        yield return new WaitForSeconds(trapDoorResetTime);
        StartCoroutine("ActivateTrapDoor");
    }

    IEnumerator ToggleTrapDoor()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine("ActivateTrapDoor");
    }
}
