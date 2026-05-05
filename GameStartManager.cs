using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public static event Action OnStartButtonClick;

    public LobbySettingSO lobbySetting; //test!

    public void ClickStartButton()
    {
        OnStartButtonClick?.Invoke();

        //test!
        Debug.Log("WordCount: " + lobbySetting.settingValue.GetValue(SettingList.WordCount));
        Debug.Log("PlayerHp: " + lobbySetting.settingValue.GetValue(SettingList.PlayerHp));
        Debug.Log("TimeLimit: " + lobbySetting.settingValue.GetValue(SettingList.TimeLimit));
        Debug.Log("EnemySpeed: " + lobbySetting.settingValue.GetValue(SettingList.EnemySpeed));
        Debug.Log("HintActiveTime: " + lobbySetting.settingValue.GetValue(SettingList.HintActiveTime));
        Debug.Log("EnemySpawnDelay: " + lobbySetting.settingValue.GetValue(SettingList.EnemySpawnDelay));
    }
}
