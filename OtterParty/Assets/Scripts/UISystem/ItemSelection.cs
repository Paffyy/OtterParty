using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject centerPos;
    [SerializeField]
    private GameObject leftPos;
    [SerializeField]
    private GameObject rightPos;
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
    private float scaleOffset;
    private int playerIndex;

    void Start()
    {
        scaleOffset = 0.7f;
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
        if (centerHat != null)
        {
            centerSprite.sprite = centerHat.GetHatSprite(playerIndex);
        }
        if (rightHat != null)
        {
            rightSprite.sprite = rightHat.GetHatSprite(playerIndex);
        }
        if(leftHat != null)
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
            centerSprite.sprite = centerHat.GetHatSprite(playerIndex);
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
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            rightHat = centerHat;
            rightSprite.sprite = rightHat.GetHatSprite(playerIndex);
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
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            leftHat = centerHat;
            leftSprite.sprite = leftHat.GetHatSprite(playerIndex);
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
            centerHat.IsAvailable = true;
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
    }
}
