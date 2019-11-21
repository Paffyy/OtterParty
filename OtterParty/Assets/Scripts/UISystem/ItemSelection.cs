using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer centerSprite;
    [SerializeField]
    private SpriteRenderer leftSprite;
    [SerializeField]
    private SpriteRenderer rightSprite;
    private PlayerController player;
    private int centerHatIndex;
    private PlayerHat selectedHat;
    private bool hatSelected;
    private GameObject hat;

    void Start()
    {
        hatSelected = false;
        player = GetComponent<PlayerController>();
        SetDefaultItems();
    }

    private void SetDefaultItems()
    {
        if(GameController.Instance.PlayerHats.Count > 0)
        {
            selectedHat = GameController.Instance.PlayerHats[0];
            centerSprite.sprite = selectedHat.HatSprite;
            hat = Instantiate(selectedHat.gameObject, player.Hat.position, player.Hat.rotation, player.Hat);
            centerHatIndex = 0;
            if(GameController.Instance.PlayerHats.Count > 1)
            {
                rightSprite.sprite = GameController.Instance.PlayerHats[1].HatSprite;
            }
        }
    }

    private void OnShiftLeft()
    {
        if (centerHatIndex - 1 >= 0 && !hatSelected)
        {
            Destroy(hat);
            rightSprite.sprite = selectedHat.HatSprite;
            centerHatIndex--;
            selectedHat = GameController.Instance.PlayerHats[centerHatIndex];
            hat = Instantiate(selectedHat.gameObject, player.Hat.position, player.Hat.rotation, player.Hat);
            centerSprite.sprite = selectedHat.HatSprite;
            //show hat on player
            if (centerHatIndex - 1 >= 0)
            {
                leftSprite.sprite = GameController.Instance.PlayerHats[centerHatIndex - 1].HatSprite;
            }
            else
            {
                leftSprite.sprite = null;
            }
        }
    }

    private void OnShiftRight()
    {
        if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count && !hatSelected)
        {
            Destroy(hat);
            leftSprite.sprite = selectedHat.HatSprite;
            centerHatIndex++;
            selectedHat = GameController.Instance.PlayerHats[centerHatIndex];
            hat = Instantiate(selectedHat.gameObject, player.Hat.position, player.Hat.rotation, player.Hat);
            centerSprite.sprite = selectedHat.HatSprite;
            //show hat on player
            if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count)
            {
                rightSprite.sprite = GameController.Instance.PlayerHats[centerHatIndex + 1].HatSprite;
            }
            else
            {
                rightSprite.sprite = null;
            }
        }
    }

    private void OnReadyUp()
    {
        Debug.Log(selectedHat.IsAvailable);
        if (selectedHat.IsAvailable && !hatSelected)
        {
            hatSelected = true;
            selectedHat.IsAvailable = false;
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
        else
        {
            // inform that hat is not available
        }

    }

    private void OnCancelReadyUp()
    {
        Debug.Log("B pressed");
        if (hatSelected)
        {
            Debug.Log("Cancelled");
            hatSelected = false;
            selectedHat.IsAvailable = true;
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
    }
}
