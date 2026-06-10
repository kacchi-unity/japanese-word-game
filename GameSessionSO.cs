using UnityEngine;

[CreateAssetMenu(fileName = "GameSessionSO", menuName = "ScriptableObject/GameSessionSO")]

public class GameSessionSO : ScriptableObject
{
    [SerializeField] private int enlightenmentPoint = 0;
    public int EnlightenmentPoint => enlightenmentPoint;

    private int systemPlayWordLimitCount = 10;
    public int SystemPlayWordLimitCount => systemPlayWordLimitCount;

    private int score = 0;

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

    public void AddEP(int value)
    {
        this.enlightenmentPoint += value;
    }

    public void ResetEP()
    {
        this.enlightenmentPoint = 0;
    }
}
