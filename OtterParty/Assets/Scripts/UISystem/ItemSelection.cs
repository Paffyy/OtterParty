using System;
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
    [SerializeField]
    private GameObject hatTakenMessage;
    private PlayerController player;
    private int centerHatIndex;
    private PlayerHat selectedHat;
    private bool hatSelected;
    private GameObject hat;
    private bool gameHasStarted;

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.TransitionEvent, CheckReadyUp);
        hatSelected = false;
        player = GetComponent<PlayerController>();
        SetDefaultItems();
    }

    private void CheckReadyUp(BaseEventInfo e)
    {
        gameHasStarted = true;
    }

    private void SetDefaultItems()
    {
        if(GameController.Instance.PlayerHats.Count > 0)
        {
            selectedHat = GameController.Instance.PlayerHats[0].GetComponent<PlayerHat>();
            centerSprite.sprite = selectedHat.HatSprite;
            centerHatIndex = 0;
            SetPlayerHat();
            if (GameController.Instance.PlayerHats.Count > 1)
            {
                rightSprite.sprite = GameController.Instance.PlayerHats[1].GetComponent<PlayerHat>().HatSprite;
            }
        }
    }

    private void OnShiftLeft()
    {
        if (centerHatIndex - 1 >= 0 && !hatSelected)
        {
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            rightSprite.sprite = selectedHat.HatSprite;
            centerHatIndex--;
            SetPlayerHat();
            centerSprite.sprite = selectedHat.HatSprite;
            if (centerHatIndex - 1 >= 0)
            {
                leftSprite.sprite = GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<PlayerHat>().HatSprite;
            }
            else
            {
                leftSprite.sprite = null;
            }
        }
    }

    private void SetPlayerHat()
    {
        selectedHat = GameController.Instance.PlayerHats[centerHatIndex].GetComponent<PlayerHat>();
        var playerVM = GameController.Instance.FindPlayerByGameObject(player.gameObject);
        if (playerVM != null)
        {
            playerVM.HatIndex = centerHatIndex;
        }
        hat = Instantiate(selectedHat.gameObject, player.HatPlaceHolder.position, player.HatPlaceHolder.rotation, player.HatPlaceHolder);
    }

    private void OnShiftRight()
    {
        if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count && !hatSelected)
        {
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            leftSprite.sprite = selectedHat.HatSprite;
            centerHatIndex++;
            SetPlayerHat();
            centerSprite.sprite = selectedHat.HatSprite;
            if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count)
            {
                rightSprite.sprite = GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<PlayerHat>().HatSprite;
            }
            else
            {
                rightSprite.sprite = null;
            }
        }
    }

    private void OnReadyUp()
    {
        if (selectedHat.IsAvailable && !hatSelected)
        {
            hatSelected = true;
            selectedHat.IsAvailable = false;
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
        else if(!hatSelected)
        {
            hatTakenMessage.SetActive(true);
        }
    }

    private void OnCancelReadyUp()
    {
        if (hatSelected && !gameHasStarted)
        {
            hatSelected = false;
            selectedHat.IsAvailable = true;
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
    }
}
