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
    private PlayerHat centerHat;
    private PlayerHat leftHat;
    private PlayerHat rightHat;
    private bool hatSelected;
    private GameObject hat;
    private bool gameHasStarted;
    private int playerIndex;
    [SerializeField]
    private AudioClip shiftItemSound;
    public bool ShiftOnCooldown { get; set; }

    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.TransitionEvent, CheckReadyUp);
        hatSelected = false;
        player = GetComponent<PlayerController>();
        playerIndex = GameController.Instance.FindPlayerByGameObject(gameObject).ID;
        SetDefaultItems();
    }

    void Update()
    {
        UpdateHatSprites();
    }

    private void UpdateHatSprites()
    {
        UpdateCenterHat();
        UpdateRightHat();
        UpdateLeftHat();
    }

    private void UpdateCenterHat()
    {
        if (centerHat != null)
        {
            if (!centerHat.IsAvailable && centerHat.SelectedByPlayerIndex != playerIndex)
            {
                centerSprite.sprite = centerHat.GetHatSprite(playerIndex);
                hatTakenMessage.SetActive(true);
            }
            else
            {
                centerSprite.sprite = centerHat.GetHatSprite(playerIndex);
                hatTakenMessage.SetActive(false);
            }
        }
    }

    private void UpdateRightHat()
    {
        if (rightHat != null)
        {
            rightSprite.sprite = rightHat.GetHatSprite(playerIndex);
        }
    }

    private void UpdateLeftHat()
    {
        if (leftHat != null)
        {
            leftSprite.sprite = leftHat.GetHatSprite(playerIndex);
        }
    }

    private void CheckReadyUp(BaseEventInfo e)
    {
        gameHasStarted = true;
    }

    private void SetDefaultItems()
    {
        if (GameController.Instance.PlayerHats.Count > 0)
        {
            centerHat = GameController.Instance.PlayerHats[0].GetComponent<PlayerHat>();
            SetCenterPosItem();
            centerHatIndex = 0;
            SetPlayerHat();
            if (GameController.Instance.PlayerHats.Count > 1)
            {
                rightHat = GameController.Instance.PlayerHats[1].GetComponent<PlayerHat>();
                rightSprite.sprite = GameController.Instance.PlayerHats[1].GetComponent<PlayerHat>().GetHatSprite(playerIndex);
            }
        }
    }

    private void SetPlayerHat()
    {
        centerHat = GameController.Instance.PlayerHats[centerHatIndex].GetComponent<PlayerHat>();
        var playerVM = GameController.Instance.FindPlayerByGameObject(player.gameObject);
        if (playerVM != null)
        {
            playerVM.HatIndex = centerHatIndex;
        }
        hat = Instantiate(centerHat.gameObject, player.HatPlaceHolder.position, centerHat.transform.rotation, player.HatPlaceHolder);
        hat.transform.localPosition = centerHat.HatOffset;
        hat.transform.localEulerAngles = centerHat.HatRotation;
        hat.GetComponent<PlayerHat>().SetHatMaterial(playerVM.ID);
    }

    private void SetCenterPosItem()
    {
        centerSprite.sprite = centerHat.GetHatSprite(playerIndex);
        if (!centerHat.IsAvailable && centerHat.SelectedByPlayerIndex != playerIndex)
        {
            hatTakenMessage.SetActive(true);
        }
        else
        {
            hatTakenMessage.SetActive(false);
        }
    }

    private void SetRightPosItem(bool hasItem)
    {
        if (hasItem)
        {
            rightSprite.sprite = rightHat.GetHatSprite(playerIndex);
        }
        else
        {
            rightHat = null;
            rightSprite.sprite = null;
        }
    }

    private void SetLeftPosItem(bool hasItem)
    {
        if (hasItem)
        {
            leftSprite.sprite = leftHat.GetHatSprite(playerIndex);
        }
        else
        {
            leftHat = null;
            leftSprite.sprite = null;
        }
    }

    private void OnShiftLeft()
    {
        if (centerHatIndex - 1 >= 0 && !hatSelected)
        {
            PlayShiftSound();
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            rightHat = centerHat;
            SetRightPosItem(true);
            centerHatIndex--;
            SetPlayerHat();
            SetCenterPosItem();
            if (centerHatIndex - 1 >= 0)
            {
                leftHat = GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<PlayerHat>();
                SetLeftPosItem(true);
            }
            else
            {
                SetLeftPosItem(false);
            }
        }
    }

    private void OnShiftRight()
    {

        if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count && !hatSelected)
        {
            PlayShiftSound();
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            leftHat = centerHat;
            SetLeftPosItem(true);
            centerHatIndex++;
            SetPlayerHat();
            SetCenterPosItem();
            if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count)
            {
                rightHat = GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<PlayerHat>();
                SetRightPosItem(true);
            }
            else
            {
                SetRightPosItem(false);
            }
        }
    }

    private void OnReadyUp()
    {
        if (centerHat.IsAvailable && !hatSelected)
        {
            hatSelected = true;
            centerHat.IsAvailable = false;
            centerHat.SelectedByPlayerIndex = playerIndex;
            SendReadyUpEvent();
        }
        else if (!hatSelected)
        {
            hatTakenMessage.SetActive(true);
        }
    }

    private void OnCancelReadyUp()
    {
        if (hatSelected && !gameHasStarted)
        {
            hatSelected = false;
            centerHat.IsAvailable = true;
            SendReadyUpEvent();
        }
    }

    private void PlayShiftSound()
    {
        SoundEventInfo sei = new SoundEventInfo(shiftItemSound, 0, 1);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
    }

    private void SendReadyUpEvent()
    {
        var id = GetComponent<PlayerInput>().playerIndex;
        ReadyUpEventInfo e = new ReadyUpEventInfo(id);
        EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
    }

}
