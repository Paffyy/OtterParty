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
    private List<PlayerTest> players;
    void Awake()
    {
        pim = GetComponent<PlayerInputManager>();
        players = new List<PlayerTest>();
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
            Debug.Log(players.Count);
            foreach (var item in players)
            {
                pim.JoinPlayer(item.Index, item.Index, null, item.Device);
            }
        }
    }
    private void OnPlayerJoined(PlayerInput obj)
    {
        if (!joined)
        {
            var p = new PlayerTest()
            {
                Index = obj.playerIndex,
                Device = obj.devices[0]
            };
            players.Add(p);
            Debug.Log("join");
        }
    }
    public class PlayerTest
    {
        public int Index { get; set; }
        public InputDevice Device { get; set; }
    }
}
