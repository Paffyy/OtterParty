using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleEliminateEventInfo : BaseEventInfo
{
    public List<GameObject> EliminatedPlayers { get; set; }

    public MultipleEliminateEventInfo(List<GameObject> players)
    {
        EliminatedPlayers = players;
    }
}
