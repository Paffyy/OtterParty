using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PodiumHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> podiumPrefabs;
    [SerializeField]
    private List<Transform> podiumPlacements;
    [SerializeField]
    private float showPlacementsDelay;
    [SerializeField]
    private float sceneDuration;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private AudioClip winnerSound;
    private PlayerInputManager playerInputManager;

    void Awake()
    {
        foreach (Transform item in gameObject.transform)
        {
            podiumPlacements.Add(item);
        }
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerPrefab;
    }

    void Start()
    {
        JoinPlayers();
        InitiateCeremony();    
    }

    public void JoinPlayers()
    {
        foreach (var item in GameController.Instance.Players)
        {
            playerInputManager.JoinPlayer(item.ID, -1, null, item.Device);
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Player player = GameController.Instance.FindPlayerByID(playerInput.playerIndex);
        player.PlayerObject = playerInput.gameObject;
        Material[] mats = new Material[] { GameController.Instance.PlayerMaterials[player.ID] };
        player.PlayerObject.GetComponent<PlayerController>().MeshRenderer.materials = mats;
        var hat = GameController.Instance.PlayerHats[player.HatIndex].GetComponent<PlayerHat>();
        var hatTransform = playerInput.GetComponent<PlayerController>().HatPlaceHolder;
        var hatClone = Instantiate(hat.gameObject, hatTransform.position, hat.transform.rotation, hatTransform);
        hatClone.transform.localPosition = hat.HatOffset;
        hatClone.transform.localEulerAngles = hat.HatRotation;
        hatClone.GetComponent<PlayerHat>().SetHatMaterial(player.ID);
        playerInput.gameObject.transform.position = podiumPlacements[playerInput.playerIndex].transform.position;
        playerInput.gameObject.transform.rotation = podiumPlacements[playerInput.playerIndex].transform.rotation;
    }
    private void InitiateCeremony()
    {
        StartCoroutine("ShowPlacements");
    }

    IEnumerator ShowPlacements()
    {
        yield return new WaitForSeconds(showPlacementsDelay);
        AssignPodiums();
        PlayWinnerSound();
        StartCoroutine("GoToMainMenu");
    }

    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(sceneDuration);
        GameController.Instance.StartNextMinigame();
    }

    private void PlayWinnerSound()
    {
        SoundEventInfo sei = new SoundEventInfo(winnerSound, 0);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
    }

    private void AssignPodiums()
    {
        PointSystem ps = GameController.Instance.PointSystem;
        foreach (var player in GameController.Instance.Players)
        {
            int placement = ps.GetCurrentScore()[player];
            Debug.Log(placement);
            GameObject podiumPrefab = podiumPrefabs[placement - 1];
            var podium = Instantiate(podiumPrefab, podiumPlacements[player.ID].position, podiumPrefab.transform.rotation);
            podium.GetComponent<Podium>().ElevatePodium();
        }
    }
}
