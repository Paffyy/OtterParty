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
    private MeshFilter centerPos;
    [SerializeField]
    private MeshFilter leftPos;
    [SerializeField]
    private MeshFilter rightPos;
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
            //centerSprite.sprite = selectedHat.HatSprite;
            centerPos.mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            centerHatIndex = 0;
            SetPlayerHat();
            if (GameController.Instance.PlayerHats.Count > 1)
            {
                rightPos.mesh = GameController.Instance.PlayerHats[1].GetComponent<MeshFilter>().sharedMesh;
                //rightSprite.sprite = GameController.Instance.PlayerHats[1].GetComponent<PlayerHat>().HatSprite;
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
        hat = Instantiate(selectedHat.gameObject, player.HatPlaceHolder.position + selectedHat.HatOffset, selectedHat.transform.rotation, player.HatPlaceHolder);
        //hat.GetComponent<PlayerHat>().SetPlayerMaterial(playerVM.ID);
    }

    private void OnShiftLeft()
    {
        if (centerHatIndex - 1 >= 0 && !hatSelected)
        {
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            rightPos.mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            //rightSprite.sprite = selectedHat.HatSprite;
            centerHatIndex--;
            SetPlayerHat();
            //centerSprite.sprite = selectedHat.HatSprite;
            centerPos.mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            if (centerHatIndex - 1 >= 0)
            {
               // leftSprite.sprite = GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<PlayerHat>().HatSprite;
               leftPos.mesh = GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<MeshFilter>().sharedMesh;
            }
            else
            {
                //  leftSprite.sprite = null;
                leftPos.mesh = null;
            }
        }
    }

    private void OnShiftRight()
    {
        if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count && !hatSelected)
        {
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            leftPos.mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            //leftSprite.sprite = selectedHat.HatSprite;
            centerHatIndex++;
            SetPlayerHat();
            // centerSprite.sprite = selectedHat.HatSprite;
            centerPos.mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count)
            {
               // rightSprite.sprite = GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<PlayerHat>().HatSprite;
                rightPos.mesh = GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<MeshFilter>().sharedMesh;
            }
            else
            {
                rightPos.mesh = null;
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
