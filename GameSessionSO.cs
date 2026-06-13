using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSessionSO", menuName = "ScriptableObject/GameSessionSO")]

public class GameSessionSO : BaseSaveSO<GameSessionSO.RuntimeData>
{
    [Header ("기본 설정 값 (불변)")]
    [Tooltip("값이 변하지 않는 기초 값 입니다.")]
    [SerializeField] private int defaultEnlightenmentPoint = 0;
    [SerializeField] private int score = 0;
    [SerializeField] private int systemPlayWordLimitCount = 10;
    public int SystemPlayWordLimitCount => systemPlayWordLimitCount;

    //Json local save class
    [Serializable]
    public class RuntimeData
    {
        public int enlightenmentPoint;
    }

    public override void Initialize()
    {
        runtimeData.enlightenmentPoint = this.defaultEnlightenmentPoint;
    }

    public int EnlightenmentPoint => runtimeData.enlightenmentPoint;

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
        runtimeData.enlightenmentPoint += value;
    }

    public void ResetEP()
    {
        runtimeData.enlightenmentPoint = 0;
    }
}
