using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolMiniGameController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] symbolSprites;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float timeToFall;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float timeToFallMultiplier;
    private Sprite symbolSprite;
    private List<SymbolPlatform> platforms = new List<SymbolPlatform>();
    private List<GameObject> eliminatedPlayers = new List<GameObject>();
    [SerializeField]
    private SymbolPlatform currentSymbolPlatform;
    [SerializeField]
    [Range(0.5f, 5f)]
    private float resetTime;
    [SerializeField]
    private AudioClip newPlatformSound;
    [SerializeField]
    private float newPlatformSoundVolume;

    void Start()
    {
        foreach (Transform item in transform)
        {
            var platform = item.gameObject.GetComponent<SymbolPlatform>();
            if(platform != null)
            {
                platforms.Add(platform);
            }
        }
        EventHandler.Instance.Register(EventHandler.EventType.StartMinigameEvent, StartGame);
        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, StopGame);  
    }

    private void AssignCurrentSymbol()
    {
        symbolSprite = symbolSprites[RandomizeSymbol()];
        currentSymbolPlatform.SetSymbol(symbolSprite);
    }

    private void AssignPlatformSymbols()
    {
        SymbolPlatform platform = platforms[Random.Range(0, platforms.Count - 1)];
        platform.SetSymbol(symbolSprite);
        platform.IsSafe = true;
        platform.HasSymbol = true;
        foreach (var item in platforms)
        {
            var newSymbol = symbolSprites[RandomizeSymbol()];
            if (!item.HasSymbol)
            {
                item.SetSymbol(newSymbol);
                if (newSymbol == symbolSprite)
                {
                    item.IsSafe = true;
                }
            }        
        }
    }

    private int RandomizeSymbol()
    {
        return Random.Range(0, symbolSprites.Length);
    }

    private void CheckPlatforms()
    {
        foreach (var item in platforms)
        {
            if (!item.IsSafe)
            {
                item.FallDown();
            }
        }
    }

    private void ResetPlatforms()
    {
        if(eliminatedPlayers.Count > 1)
        {
            MultipleEliminateEventInfo multipleEliminateEventInfo = new MultipleEliminateEventInfo(eliminatedPlayers);
            EventHandler.Instance.FireEvent(EventHandler.EventType.MultipleEliminateEvent, multipleEliminateEventInfo);
        }
        else if (eliminatedPlayers.Count == 1)
        {
            EliminateEventInfo eliminateEventInfo = new EliminateEventInfo(eliminatedPlayers[0]);
            EventHandler.Instance.FireEvent(EventHandler.EventType.EliminateEvent, eliminateEventInfo);
        }
        eliminatedPlayers.Clear();
        currentSymbolPlatform.ResetPlatform();
        foreach (var item in platforms)
        {
            item.ResetPlatform();
            item.IsSafe = false;
        }
    }

    private void StartGame(BaseEventInfo e)
    {
        StartCoroutine("SymbolGameLoop");
    }

    IEnumerator SymbolGameLoop()
    {
        AssignCurrentSymbol();
        AssignPlatformSymbols();
        SoundEventInfo sei = new SoundEventInfo(newPlatformSound, newPlatformSoundVolume, 1);
        EventHandler.Instance.FireEvent(EventHandler.EventType.SoundEvent, sei);
        yield return new WaitForSeconds(timeToFall);
        CheckPlatforms();
        timeToFall *= timeToFallMultiplier;
        StartCoroutine("ResetGame");
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(resetTime);
        ResetPlatforms();
        StartCoroutine("SymbolGameLoop");
    }

    private void StopGame(BaseEventInfo e)
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!eliminatedPlayers.Contains(other.gameObject))
            {
                eliminatedPlayers.Add(other.gameObject);
            }
        }
    }
}

