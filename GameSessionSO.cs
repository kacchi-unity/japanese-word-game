using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameSessionSO", menuName = "ScriptableObject/GameSessionSO")]

public class GameSessionSO : ScriptableObject
{
    private int score;

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return this.score;
    }

    public void ResetScore()
    {
        this.score = 0;
    }
}
