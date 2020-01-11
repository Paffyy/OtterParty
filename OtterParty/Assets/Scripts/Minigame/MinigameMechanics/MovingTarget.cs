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

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * ImportManager.Instance.Settings.ChickenShootoutSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        
    }

    public void SetValue(int pointValue)
    {
        Points = pointValue;
    }

    public GameObject GetPlayerParticle(int index)
    {
        if(particles.Count - 1 >= index)
        {
            return particles[index];
        }
        else
        {
            return null;
        }

    }
}
