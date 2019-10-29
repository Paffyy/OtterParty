using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimatedImage : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> spritesToLoop;
    [SerializeField]
    private float interval;
    private int counter;
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
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
