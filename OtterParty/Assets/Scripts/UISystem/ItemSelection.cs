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
    private GameObject hatTakenMessage;
    private PlayerController player;
    private int centerHatIndex;
    private PlayerHat selectedHat;
    private bool hatSelected;
    private GameObject hat;
    private bool gameHasStarted;
    private float scaleOffset;

    void Start()
    {
        scaleOffset = 0.7f;
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
        if (GameController.Instance.PlayerHats.Count > 0)
        {
            selectedHat = GameController.Instance.PlayerHats[0].GetComponent<PlayerHat>();       
            centerPos.GetComponent<MeshFilter>().mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            centerPos.GetComponent<MeshRenderer>().material = selectedHat.HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
            centerPos.transform.localScale = 1 * selectedHat.ThumnailScale;
            centerHatIndex = 0;
            SetPlayerHat();
            if (GameController.Instance.PlayerHats.Count > 1)
            {
                rightPos.GetComponent<MeshFilter>().mesh = GameController.Instance.PlayerHats[1].GetComponent<MeshFilter>().sharedMesh;
                rightPos.GetComponent<MeshRenderer>().material = selectedHat.HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
                rightPos.transform.localScale = scaleOffset * selectedHat.ThumnailScale;
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
        hat = Instantiate(selectedHat.gameObject, player.HatPlaceHolder.position, selectedHat.transform.rotation, player.HatPlaceHolder);
        hat.transform.localPosition = selectedHat.HatOffset;
        hat.transform.localEulerAngles = selectedHat.HatRotation;
        hat.GetComponent<PlayerHat>().SetPlayerMaterial(playerVM.ID);
    }

    private void SetCenterPosItem()
    {
        centerPos.GetComponent<MeshFilter>().mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
        centerPos.GetComponent<MeshRenderer>().material = selectedHat.HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
        centerPos.transform.localScale = 1 * selectedHat.ThumnailScale;
    }

    private void OnShiftLeft()
    {
        if (centerHatIndex - 1 >= 0 && !hatSelected)
        {
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            rightPos.GetComponent<MeshFilter>().mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            rightPos.GetComponent<MeshRenderer>().material = selectedHat.HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
            rightPos.transform.localScale = scaleOffset * selectedHat.ThumnailScale;
            centerHatIndex--;
            SetPlayerHat();
            SetCenterPosItem();
            if (centerHatIndex - 1 >= 0)
            {
               leftPos.GetComponent<MeshFilter>().mesh = GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<MeshFilter>().sharedMesh;
               leftPos.GetComponent<MeshRenderer>().material = GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<PlayerHat>().HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
               leftPos.transform.localScale = scaleOffset * GameController.Instance.PlayerHats[centerHatIndex - 1].GetComponent<PlayerHat>().ThumnailScale;
            }
            else
            {
                leftPos.GetComponent<MeshFilter>().mesh = null;
            }
        }
    }

    private void OnShiftRight()
    {
        if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count && !hatSelected)
        {
            hatTakenMessage.SetActive(false);
            Destroy(hat);
            leftPos.GetComponent<MeshFilter>().mesh = selectedHat.gameObject.GetComponent<MeshFilter>().sharedMesh;
            leftPos.GetComponent<MeshRenderer>().material = selectedHat.HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
            leftPos.transform.localScale = scaleOffset * selectedHat.ThumnailScale;
            centerHatIndex++;
            SetPlayerHat();
            SetCenterPosItem();
            if (centerHatIndex + 1 < GameController.Instance.PlayerHats.Count)
            {
                rightPos.GetComponent<MeshFilter>().mesh = GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<MeshFilter>().sharedMesh;
                rightPos.GetComponent<MeshRenderer>().material = GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<PlayerHat>().HatMaterials[GameController.Instance.FindPlayerByGameObject(gameObject).ID];
                rightPos.transform.localScale = scaleOffset * GameController.Instance.PlayerHats[centerHatIndex + 1].GetComponent<PlayerHat>().ThumnailScale;
            }
            else
            {
                rightPos.GetComponent<MeshFilter>().mesh = null;
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
