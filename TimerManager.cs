using System;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static event Action OnTimeZero;
    public TextMeshProUGUI timer;

    float time_float;
    bool isTimerActive = false;

    private void OnEnable()
    {
        WordBoardButtonManager.OnBattleStartButtonClick += BattleStartEventHandling;
    }

    private void OnDisable()
    {
        WordBoardButtonManager.OnBattleStartButtonClick -= BattleStartEventHandling;
    }

    void BattleStartEventHandling()
    {
        this.isTimerActive = true;
    }

    void Awake()
    {
        time_float = GameDataManager.Instance.RuntimeLobbySetting.GetValue(SettingList.TimeLimit);
        isTimerActive = false;
    }

    void Update()
    {
        if (isTimerActive)
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
}
