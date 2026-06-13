using UnityEngine;
using System;

public enum SettingList
{   None,
    WordCount, 
    PlayerHp, 
    TimeLimit, 
    EnemySpeedRate, 
    HintActiveTime, 
    EnemySpawnDelay 
}

[CreateAssetMenu(fileName = "LobbySettingSO", menuName = "ScriptableObject/LobbySettingSO")]

public class LobbySettingSO : BaseSaveSO<LobbySettingSO.RuntimeData>
{
    [Header("기본 설정 값 (불변)")]
    [Tooltip("값이 변하지 않는 기초 값 입니다.")]
    [SerializeField] private float defaultWordCount = 3f;
    [SerializeField] private float defaultPlayerHp = 10f;
    [SerializeField] private float defaultTimeLimit = 60f;
    [SerializeField] private float defaultEnemySpeedRate = 0.3f;
    [SerializeField] private float defaultHintActiveTime = 10f;
    [SerializeField] private float defaultEnemySpawnDelay = 2f;

    //Json local save class
    [Serializable]
    public class RuntimeData
    {
        public float wordCount;
        public float playerHp;
        public float timeLimit;
        public float enemySpeedRate;
        public float hintActiveTime;
        public float enemySpawnDelay;
    }

    public override void Initialize()
    {
        runtimeData.wordCount = this.defaultWordCount;
        runtimeData.playerHp = this.defaultPlayerHp;
        runtimeData.timeLimit = this.defaultTimeLimit;
        runtimeData.enemySpeedRate = this.defaultEnemySpeedRate;
        runtimeData.hintActiveTime = this.defaultHintActiveTime;
        runtimeData.enemySpawnDelay = this.defaultEnemySpawnDelay;
    }

    public float GetValue(SettingList target) => target switch
    {
        SettingList.WordCount => runtimeData.wordCount,
        SettingList.PlayerHp => runtimeData.playerHp,
        SettingList.TimeLimit => runtimeData.timeLimit,
        SettingList.EnemySpeedRate => runtimeData.enemySpeedRate,
        SettingList.HintActiveTime => runtimeData.hintActiveTime,
        SettingList.EnemySpawnDelay => runtimeData.enemySpawnDelay,
        _ => 0f
    };

    public void SetValue(SettingList target, float value)
    {
        switch (target)
        {
            case SettingList.WordCount: runtimeData.wordCount = value; break;
            case SettingList.PlayerHp: runtimeData.playerHp = value; break;
            case SettingList.TimeLimit: runtimeData.timeLimit = value; break;
            case SettingList.EnemySpeedRate: runtimeData.enemySpeedRate = value; break;
            case SettingList.HintActiveTime: runtimeData.hintActiveTime = value; break;
            case SettingList.EnemySpawnDelay: runtimeData.enemySpawnDelay = value; break;
            default: Debug.LogWarning($"{target} is not defined"); break;
        }
    }
}
