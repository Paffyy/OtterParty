using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cooldownDuration;
    [SerializeField]
    private Sprite[] playerCrosshairs;
    [SerializeField]
    private GameObject[] projectiles;
    private Vector2 inputDirection;
    private Vector2 bounds;
    private GameObject particleObject;
    private GameObject projectilePrefab;
    public bool IsOffCooldown { get; set; } = true;

    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (GameController.Instance != null)
        {
            var playerID = GameController.Instance.FindPlayerByGameObject(gameObject).ID;
            projectilePrefab = projectiles[playerID];
            SetImage(playerCrosshairs[playerID]);
        }
        else
        {
            projectilePrefab = projectiles[0];
            SetImage(playerCrosshairs[0]);
        }
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
    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        inputDirection = input;
    }
    private void OnFire()
    {
        if (IsOffCooldown)
        {
            var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));
            if (Physics.Raycast(ray.origin, ray.direction, out var hit))
            {
                var direction = hit.point - transform.position;
                var rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(direction),1);
                Instantiate(projectilePrefab, transform.position, rotation);
            }
            Cooldown.Instance.StartNewCooldown(cooldownDuration, this);
            IsOffCooldown = false;
        }

    }

    public void SetImage(Sprite spr)
    {
        GetComponent<SpriteRenderer>().sprite = spr;
    }
}
