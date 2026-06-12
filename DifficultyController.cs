using System;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public LobbySettingSO lobbySetting;

    public static event Action OnDifficultyChanged;

    public enum Difficulty { Easy, Normal, Hard }

    Dictionary<SettingList, float> valueDic_Easy, valueDic_Normal, valueDic_Hard;
    Dictionary<Difficulty, Dictionary<SettingList, float>> difficulty_ValueDic;

    void Awake()
    {
        valueDic_Easy = new Dictionary<SettingList, float>
        {
            [SettingList.WordCount] = 3,
            [SettingList.PlayerHp] = 30,
            [SettingList.TimeLimit] = 60,
            [SettingList.EnemySpeedRate] = 0,
            [SettingList.HintActiveTime] = 60,
            [SettingList.EnemySpawnDelay] = 3
        };

        valueDic_Normal = new Dictionary<SettingList, float>
        {
            [SettingList.WordCount] = 5,
            [SettingList.PlayerHp] = 15,
            [SettingList.TimeLimit] = 175,
            [SettingList.EnemySpeedRate] = 0.5f,
            [SettingList.HintActiveTime] = 30,
            [SettingList.EnemySpawnDelay] = 2
        };

        valueDic_Hard = new Dictionary<SettingList, float>
        {
            [SettingList.WordCount] = 10,
            [SettingList.PlayerHp] = 3,
            [SettingList.TimeLimit] = 300,
            [SettingList.EnemySpeedRate] = 1f,
            [SettingList.HintActiveTime] = 0,
            [SettingList.EnemySpawnDelay] = 1
        };

        difficulty_ValueDic = new Dictionary<Difficulty, Dictionary<SettingList, float>>()
        {
            { Difficulty.Easy, valueDic_Easy },
            { Difficulty.Normal, valueDic_Normal },
            { Difficulty.Hard, valueDic_Hard }
        };
    }

    public void ClickDifficultyButton(int index)
    {
        if (Enum.IsDefined(typeof(Difficulty), index))
        {
            Difficulty difficultyKey = (Difficulty)index;
            Dictionary<SettingList, float> targetDic = difficulty_ValueDic[difficultyKey];

            foreach (SettingList key in targetDic.Keys)
            {
                GameDataManager.Instance.RuntimeLobbySetting.SetValue(key, targetDic[key]);
            }
            
            OnDifficultyChanged?.Invoke();
        }

        else
        {
            Debug.Log($"정의되지 않은 난이도 인덱스: { index }");
        }
    }
}
