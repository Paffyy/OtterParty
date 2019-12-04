using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerSpawnTest : MonoBehaviour
{
    private PlayerInputManager pim;
    [SerializeField]
    private GameObject prefab;
    private bool joined;
    private List<Player> players;
    [SerializeField]
    private Transform spawnPoint;
    void Awake()
    {
        pim = GetComponent<PlayerInputManager>();
        players = new List<Player>();
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            var players1 = GameObject.FindGameObjectsWithTag("Player");
            foreach (var item in players1)
            {
                Destroy(item);
            }
            joined = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var item in players)
            {
                if (item.Name.Contains("Keyboard"))
                {
                    pim.JoinPlayer(item.ID, item.ID, null, item.Device);
                }
            }
        }
    }
    private void OnPlayerJoined(PlayerInput obj)
    {
        if (!joined)
        {
            var gObject = obj.gameObject;
            gObject.transform.position = spawnPoint.position;
            var p = new Player()
            {
                ID = obj.playerIndex,
                Device = obj.devices[0],
            };
            players.Add(p);
        }
    }
}
