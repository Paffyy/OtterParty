using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private List<GameObject> particles;
    [SerializeField]
    private AudioClip hitSound;
    public AudioClip HitSound { get { return hitSound; } }
    public int Points { get; set; }

    [SerializeField]
    private SpriteRenderer sprite;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        
    }

    public void SetValue(Sprite spriteValue, int pointValue)
    {
        sprite.sprite = spriteValue;
        Points = pointValue;
    }

    public GameObject GetPlayerParticle(int index)
    {
        return particles[index];
    }
}
