using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> spritesToLoop;
    [SerializeField]
    private float interval;
    private int counter;
    private SpriteRenderer image;
    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine("ShowNextPicture");
    }

    IEnumerator ShowNextPicture()
    {
        yield return new WaitForSeconds(interval);
        image.sprite = spritesToLoop[counter];
        counter++;
        if (counter >= spritesToLoop.Count)
        {
            counter = 0;
        }
        StartCoroutine("ShowNextPicture");
    }
}
