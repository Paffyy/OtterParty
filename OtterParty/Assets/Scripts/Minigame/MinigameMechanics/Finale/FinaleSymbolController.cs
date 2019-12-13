using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleSymbolController : MonoBehaviour
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
    [SerializeField]
    private List<SymbolPlatform> currentSymbolPlatforms;
    [SerializeField]
    [Range(0.5f, 5f)]
    private float resetTime;

    void Start()
    {
        foreach (Transform item in transform)
        {
            var platform = item.gameObject.GetComponent<SymbolPlatform>();
            if (platform != null)
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
        foreach (var symbolPlatform in currentSymbolPlatforms)
        {
            symbolPlatform.SetSymbol(symbolSprite);
        }
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
        foreach (var symbolPlatform in currentSymbolPlatforms)
        {
            symbolPlatform.ResetPlatform();
        }
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
}
