using UnityEngine;
using System;
using System.Collections.Generic;

public enum SettingList { WordCount, PlayerHp, TimeLimit, EnemySpeed, HintActiveTime, EnemySpawnDelay }

[CreateAssetMenu(fileName = "LobbySettingSO", menuName = "ScriptableObject/LobbySettingSO")]

public class LobbySettingSO : ScriptableObject
{
    public SettingValue settingValue = new SettingValue();

    public class SettingValue
    {

        private float wordCount, playerHp, timeLimit, enemySpeed, hintActiveTime, enemySpawnDelay;

        public float GetValue(SettingList target) => target switch
        {
            SettingList.WordCount => this.wordCount,
            SettingList.PlayerHp => this.playerHp,
            SettingList.TimeLimit => this.timeLimit,
            SettingList.EnemySpeed => this.enemySpeed,
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
                case SettingList.EnemySpeed: enemySpeed = value; break;
                case SettingList.HintActiveTime: hintActiveTime = value; break;
                case SettingList.EnemySpawnDelay: enemySpawnDelay = value; break;
                default: Debug.LogWarning($"{target} is not defined"); break;
            }
        }
    }
}
