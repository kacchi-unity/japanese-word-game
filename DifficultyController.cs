using System;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{

    public static event Action<SettingValues> OnDifficultyChanged;

    public enum Difficulty { Easy, Normal, Hard }

    public enum Setting { WordCount, PlayerHp, TimeLimit, EnemySpeed, HintActiveTime }

    public class SettingValues
    {
        public SettingValues(float wordCount_init, float playerHp_init, float timeLimit_init, float enemySpeed_init, float hintActiveTime_init)
        {
            this.wordCount = wordCount_init;
            this.playerHp = playerHp_init;
            this.timeLimit = timeLimit_init;
            this.enemySpeed = enemySpeed_init; 
            this.hintActiveTime = hintActiveTime_init;
        }

        public float wordCount, playerHp, timeLimit, enemySpeed, hintActiveTime;

        public float GetValue(Setting target) => target switch
        {
            Setting.WordCount => this.wordCount,
            Setting.PlayerHp => this.playerHp,
            Setting.TimeLimit => this.timeLimit,
            Setting.EnemySpeed => this.enemySpeed,
            Setting.HintActiveTime => this.hintActiveTime,
            _ => 0f
        };
    }

    private readonly Dictionary<Difficulty, SettingValues> difSettings = new Dictionary<Difficulty, SettingValues>(3)
    {
        { Difficulty.Easy, new SettingValues(1, 1, 20, 1, 60) }, 
        { Difficulty.Normal, new SettingValues(5, 25, 175, 5, 30) },
        { Difficulty.Hard, new SettingValues(10, 50, 300, 10, 0) }
    };



    public void ClickDifficultyButton(int index)
    {
        if (Enum.IsDefined(typeof(Difficulty), index))
        {
            Difficulty difficultyKey = (Difficulty)index;
            OnDifficultyChanged?.Invoke(this.difSettings[difficultyKey]);
        }

        else
        {
            Debug.Log($"정의되지 않은 난이도 인덱스: { index }");
        }
    }
}
