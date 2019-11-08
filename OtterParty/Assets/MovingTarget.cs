using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public int Points { get; set; }
    [SerializeField]
    private SpriteRenderer sprite;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    public void SetValue(Sprite spriteValue, int pointValue)
    {
        sprite.sprite = spriteValue;
        Points = pointValue;
    }
}
