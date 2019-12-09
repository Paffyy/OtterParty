using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private Sprite[] playerCrosshairs;
    [SerializeField] private GameObject[] hitEffects;
    private GameObject hitEffect;
    private Vector2 inputDirection;
    private Vector2 bounds;
    public bool IsOffCooldown { get; set; } = true;
    private bool isActive = true;
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (GameController.Instance != null)
        {
            var playerID = GetComponent<PlayerInput>().playerIndex;
            SetImage(playerCrosshairs[playerID]);
            hitEffect = hitEffects[playerID];
        }
        else
        {
            SetImage(playerCrosshairs[0]);
            hitEffect = hitEffects[0];
        }

        EventHandler.Instance.Register(EventHandler.EventType.EndMinigameEvent, SetInactive);
    }
    void Update()
    {
        Vector3 direction = inputDirection * speed * Time.deltaTime;
        transform.position += direction;
    }
    void LateUpdate()
    {
        var newPosition = transform.position;
        var objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        var objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        newPosition.x = Mathf.Clamp(newPosition.x, bounds.x + objectHeight, bounds.x * -1 - objectWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.y + objectHeight, bounds.y * -1 - objectHeight);
        transform.position = newPosition;
    }
    private void SetInactive(BaseEventInfo e)
    {
        isActive = true;
    }
    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        inputDirection = input;
    }
    //private void OnFire()
    //{
    //    if (IsOffCooldown && isActive)
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));
    //        if (Physics.Raycast(ray.origin, ray.direction, out var hit))
    //        {
    //            var direction = hit.point - transform.position;
    //            var rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(direction),1);
    //            var obj = Instantiate(projectilePrefab, transform.position, rotation);
    //            obj.GetComponent<ProjectileMove>().PlayerThatShot = gameObject;
    //        }
    //        Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
    //        IsOffCooldown = false;
    //    }
    //}

    private void OnFire()
    {
        if (IsOffCooldown && isActive)
        {
            var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));
            if (Physics.Raycast(ray.origin, ray.direction, out var hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    var e = new HitEventInfo(gameObject, hit.collider.gameObject);
                    EventHandler.Instance.FireEvent(EventHandler.EventType.HitEvent, e);
                }
                if (hitEffect != null)
                {
                    var hitClone = Instantiate(hitEffect, hit.point, Quaternion.identity);
                    var psHit = hitClone.GetComponent<ParticleSystem>();
                    if (psHit != null)
                    {
                        Destroy(hitClone, psHit.main.duration);
                    }
                    else
                    {
                        var psChild = hitClone.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitClone, psChild.main.duration);
                    }
                }
            }
            Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
            IsOffCooldown = false;
        }
    }

    private void OnReadyUp()
    {
        ReadyUp();
    }
    private void ReadyUp()
    {
        if (EventHandler.Instance != null)
        {
            var id = GetComponent<PlayerInput>().playerIndex;
            ReadyUpEventInfo e = new ReadyUpEventInfo(id);
            EventHandler.Instance.FireEvent(EventHandler.EventType.ReadyUpEvent, e);
        }
    }

    public void SetImage(Sprite spr)
    {
        GetComponent<SpriteRenderer>().sprite = spr;
    }
}
