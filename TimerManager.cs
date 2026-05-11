using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TimerManager : MonoBehaviour
{
    public static event Action OnTimeZero;
    public TextMeshProUGUI timer;
    public LobbySettingSO lobbySetting;
    float time_float;

    void Awake()
    {
        time_float = lobbySetting.settingValue.GetValue(SettingList.TimeLimit);
    }

    void Update()
    {
        if (time_float > 0)
        {
            this.time_float -= Time.deltaTime;
            this.timer.text = $"{this.time_float.ToString("F0")} 초";
        }

        else
        {
            this.timer.text = $"{0} 초";
            OnTimeZero?.Invoke();
            this.enabled = false;

        }

    }
}
