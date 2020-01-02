using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitListener : BaseListener
{
    private Dictionary<GameObject, int> playerScore = new Dictionary<GameObject, int>();
    private bool gameIsActive;

    public override void Register()
    {
        gameIsActive = true;
        EventHandler.Instance.Register(EventHandler.EventType.HitEvent, EnemyHit);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopAddingPoints);
    }

    private void StopAddingPoints(BaseEventInfo e)
    {
        gameIsActive = false;
    }

    private void EnemyHit(BaseEventInfo e)
    {
        HitEventInfo eventInfo = e as HitEventInfo;
        GameObject playerThatShot = eventInfo.ObjectThatFired;
        GameObject hitObject = eventInfo.ObjectHit;
        if(hitObject.GetComponent<PinataBehaviour>() != null)
        {
            HandlePinataHitEvent(hitObject, playerThatShot);
        }
        else if (hitObject.GetComponent<MovingTarget>() != null)
        {
            HandleChickenShootingGalleryHitEvent(hitObject, playerThatShot);
        }
        if (eventInfo != null && !eventInfo.ObjectHit.CompareTag("Player"))
        {
            if (gameIsActive)
            {
                int pts = 1;
                if (hitObject.GetComponent<PinataBehaviour>() != null)
                {
                    pts = hitObject.GetComponent<PinataBehaviour>().Points;
                }
                else if (hitObject.GetComponent<MovingTarget>() != null)
                {
                    pts = hitObject.GetComponent<MovingTarget>().Points;
                }
                AssignPoints(playerThatShot, pts);
                Player p = GameController.Instance.FindPlayerByGameObject(playerThatShot);
                var points = new Dictionary<Player, int>();
                points.Add(p, pts);
                MinigameController.Instance.MinigamePointSystem.UpdateScore(points);
                UpdatePlayerScoreEventInfo updateEventInfo = new UpdatePlayerScoreEventInfo() { Player = playerThatShot, Score = playerScore[playerThatShot] };
                EventHandler.Instance.FireEvent(EventHandler.EventType.UpdateScoreEvent, updateEventInfo);
            }
            Destroy(hitObject);
        }
    }

    private void HandlePinataHitEvent(GameObject hitObject, GameObject playerThatShot)
    {
        Vector3 position = hitObject.transform.position;
        Quaternion rotation = hitObject.transform.rotation;
        GameObject deathParticles = hitObject.GetComponent<PinataBehaviour>().ParticleObjects[GameController.Instance.FindPlayerByGameObject(playerThatShot).ID];
        TransformEventInfo tei = new TransformEventInfo(position, rotation, deathParticles);
        EventHandler.Instance.FireEvent(EventHandler.EventType.ParticleEvent, tei);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(hitObject.GetComponent<PinataBehaviour>().HitAudio, hitObject.GetComponent<PinataBehaviour>().HitAudioVolume, 1));
    }

    private void HandleChickenShootingGalleryHitEvent(GameObject hitObject, GameObject playerThatShot)
    {
        Vector3 position = hitObject.transform.position;
        EventHandler.Instance.FireEvent(EventHandler.EventType.ParticleEvent, new TransformEventInfo(position, Quaternion.identity, hitObject.GetComponent<MovingTarget>().GetPlayerParticle(GameController.Instance.FindPlayerByGameObject(playerThatShot).ID)));
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, new SoundEventInfo(hitObject.GetComponent<MovingTarget>().HitSound, 1f, 1));
    }

    private void AssignPoints(GameObject player, int points)
    {
        if (!playerScore.ContainsKey(player))
        {
            playerScore.Add(player, points);
        }
        else
        {
            playerScore[player] += points;
        }
    }
}
