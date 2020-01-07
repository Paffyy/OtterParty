using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Player Player { get; set; }
    public Image PlayerImage
    {
        get { return playerImage; }
        set { playerImage = value; }
    }

    [SerializeField]
    [Range(1,3)]
    private int addDelay;
    [SerializeField]
    private Image playerImage;
    [SerializeField]
    private TextMeshProUGUI currentPointsText;
    [SerializeField]
    private TextMeshProUGUI pointsToAddText;
    private int finalScore;
    public void UpdatePoints(int currentPoints, int pointsToAdd)
    {
        currentPointsText.text = currentPoints.ToString();
        pointsToAddText.text = "+" + pointsToAdd;
        finalScore = currentPoints + pointsToAdd;
        StartCoroutine("PlayAddAnimation");
    }
    IEnumerator PlayAddAnimation()
    {
        yield return new WaitForSeconds(addDelay);
        pointsToAddText.GetComponent<Animator>().SetTrigger("Start");

    }
    
    public void UpdateFinalScore(int score)
    {
        currentPointsText.text = score.ToString();
    }
    public void UpdateScore()
    {
        currentPointsText.text = finalScore.ToString();
    }
}
