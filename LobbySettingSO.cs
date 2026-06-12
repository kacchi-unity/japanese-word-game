using UnityEngine;
using System;

public enum SettingList { None, WordCount, PlayerHp, TimeLimit, EnemySpeedRate, HintActiveTime, EnemySpawnDelay }

[CreateAssetMenu(fileName = "LobbySettingSO", menuName = "ScriptableObject/LobbySettingSO")]

public class LobbySettingSO : ScriptableObject
{
    public DefaultLobbySetting defaultLobbySetting = new DefaultLobbySetting();

    [Serializable]
    public class DefaultLobbySetting
    {
        public float wordCount;
        public float playerHp;
        public float timeLimit;
        public float enemySpeedRate;
        public float hintActiveTime;
        public float enemySpawnDelay;
    }
}

[Serializable]
public class RuntimeLobbySetting
{
    [SerializeField] private float wordCount;
    [SerializeField] private float playerHp;
    [SerializeField] private float timeLimit;
    [SerializeField] private float enemySpeedRate;
    [SerializeField] private float hintActiveTime;
    [SerializeField] private float enemySpawnDelay;

    public float GetValue(SettingList target) => target switch
    {
        SettingList.WordCount => this.wordCount,
        SettingList.PlayerHp => this.playerHp,
        SettingList.TimeLimit => this.timeLimit,
        SettingList.EnemySpeedRate => this.enemySpeedRate,
        SettingList.HintActiveTime => this.hintActiveTime,
        SettingList.EnemySpawnDelay => this.enemySpawnDelay,
        _ => 0f
    };

    public void SetValue(SettingList target, float value)
    {
        switch (target)
        {
            case SettingList.WordCount: wordCount = value; break;
            case SettingList.PlayerHp: playerHp = value; break;
            case SettingList.TimeLimit: timeLimit = value; break;
            case SettingList.EnemySpeedRate: enemySpeedRate = value; break;
            case SettingList.HintActiveTime: hintActiveTime = value; break;
            case SettingList.EnemySpawnDelay: enemySpawnDelay = value; break;
            default: Debug.LogWarning($"{target} is not defined"); break;
        }
    }
}
