using UnityEngine;
using TMPro;
using NUnit.Framework;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int amount;
    GameObject scoreUI;

    public void increaseScore(int amount)
    {
        score += amount;
        scoreUI.GetComponent<TextMeshProUGUI>().text = "점수: " + score.ToString("D3");
    }

    

    void Start()
    {
        scoreUI = GameObject.Find("Score");
        scoreUI.GetComponent<TextMeshProUGUI>().text = "점수: " + score.ToString("D3");
    }

    void Update()
    {
        
    }
}
