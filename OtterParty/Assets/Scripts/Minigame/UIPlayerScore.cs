using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class UIPlayerScore : MonoBehaviour
{
    [SerializeField]
    [Range(1, 4)]
    private int placementUpdateDelay;
    public bool IsPointsBased { get; set; }

    private void Start()
    {
        InitPointsUI();
    }

    public virtual void InitPointsUI() { }
    public virtual void UpdatePlacements() { }

    public List<KeyValuePair<Player, int>> SortByAscending(Dictionary<Player, int> pointDictionary)
    {
        var sorted = from playerScore
                     in pointDictionary
                     orderby -playerScore.Value
                     select playerScore;
        return sorted.ToList();
    }

    public List<KeyValuePair<Player, float>> SortByAscending(Dictionary<Player, float> pointDictionary)
    {
        var sorted = from playerScore
                     in pointDictionary
                     orderby -playerScore.Value
                     select playerScore;
        return sorted.ToList();
    }
    public IEnumerator UpdatePlacement()
    {
        yield return new WaitForSeconds(placementUpdateDelay);
        UpdatePlacements();
    }

}
