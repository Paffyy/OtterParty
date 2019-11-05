using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.Transition<ShootingState>();
            player.PlayerGun.SetActive(true);
            player.FirePoint.gameObject.SetActive(true);
        }
    }
}
