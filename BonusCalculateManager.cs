using System;
using System.Collections.Generic;
using UnityEngine;

public enum Bonus
{
    WordCount,
    PlayerHp,
    EnemySpeedRate,
    EnemySpawnDelay,
    TimeLimit,
    HintActiveTime,
}

public enum Result
{ 
    Score,
    Total,
    Reward
}

public class BonusCalculateManager : MonoBehaviour
{
    public BonusTableSO bonusTable;
    public GameSessionSO gameSession;
    public static event Action<Dictionary<Bonus, float>> OnBonusCalculationComplete;
    public static event Action<Dictionary<Result, float>> OnResultCalculationComplete;

    private float totalBonus = 0f;
    private float reward;
    private int playerScore;
    [SerializeField] private float minBonus = 0.02f;

    private Dictionary<Bonus, float> bonusDictionary = new Dictionary<Bonus, float>();
    private Dictionary<Result, float> resultDictionary = new Dictionary<Result, float>();

    public void Start()
    {
        bonusDictionary.Clear();
        resultDictionary.Clear();

        LobbySettingSO runtimeLobbySetting = GameDataManager.Instance.GetData<LobbySettingSO>();
        BonusTableSO.RewardBonusTable bonusSO = bonusTable.rewardBonuse;
        this.playerScore = gameSession.GetScore();

        //Increase type - Using "Min" value
        bonusDictionary[Bonus.WordCount]
            = bonusSO.WordCount_Min + (runtimeLobbySetting.GetValue(SettingList.WordCount) - 1f) * bonusSO.WordCount_Multiplier;

        bonusDictionary[Bonus.TimeLimit]
            = bonusSO.TimeLimit_Min + runtimeLobbySetting.GetValue(SettingList.TimeLimit) * bonusSO.TimeLimit_Multiplier;

        //Ratio type - Using "Min" value
        bonusDictionary[Bonus.EnemySpeedRate]
            = bonusSO.EnemySpeedRate_Min + runtimeLobbySetting.GetValue(SettingList.EnemySpeedRate) * bonusSO.EnemySpeedRate_Multiplier;

        //Deduction type - Using "Base" value
        float tmp;

        tmp = bonusSO.PlayerHp_Base - runtimeLobbySetting.GetValue(SettingList.PlayerHp) * bonusSO.PlayerHp_Multiplier;
        bonusDictionary[Bonus.PlayerHp] = Mathf.Max(tmp, this.minBonus);

        tmp = bonusSO.EnemySpawnDelay_Base - runtimeLobbySetting.GetValue(SettingList.EnemySpawnDelay) * bonusSO.EnemySpawnDelay_Multiplier;
        bonusDictionary[Bonus.EnemySpawnDelay] = Mathf.Clamp(tmp, this.minBonus, bonusSO.EnemySpawnDelay_Base);
            
        tmp = bonusSO.HintActiveTime_Base - runtimeLobbySetting.GetValue(SettingList.HintActiveTime) * bonusSO.HintActiveTime_Multiplier;
        bonusDictionary[Bonus.HintActiveTime] = Mathf.Clamp(tmp, this.minBonus, bonusSO.HintActiveTime_Base);

        //Final Calculation
        this.totalBonus = 0f;
        
        foreach (var targetItem in bonusDictionary)
        {
            this.totalBonus += targetItem.Value;
        }
        OnBonusCalculationComplete?.Invoke(this.bonusDictionary);

        this.reward = Mathf.Floor(this.playerScore * (1f + this.totalBonus) + 0.5f);

        resultDictionary[Result.Total] = this.totalBonus;

        resultDictionary[Result.Score] = this.playerScore;

        resultDictionary[Result.Reward] = this.reward;

        OnResultCalculationComplete?.Invoke(this.resultDictionary);

    }
}
