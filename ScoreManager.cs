using UnityEngine;
using TMPro;
using NUnit.Framework;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTMPro;
    public GameSessionSO gameSessionSO;
    
    public void IncreaseScore(int amount)
    {
        this.gameSessionSO.AddScore(amount);
        scoreTMPro.text = $"점수: {this.gameSessionSO.GetScore()}";
    }

    void Start()
    {
        this.gameSessionSO.ResetScore();
        scoreTMPro.text = $"점수: {this.gameSessionSO.GetScore()}";
    }

}
