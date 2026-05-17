using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordCardSetting : MonoBehaviour
{
    public TextMeshProUGUI kanjiTMP;
    public TextMeshProUGUI meaningTMP;
    public TextMeshProUGUI correctRateTMP;
    public TextMeshProUGUI idTMP;
    public Toggle selectToggle;

    public int wordId;

    //To find this object, only one
    public static event Action<WordCardSetting, int> OnCardSpawned;
    //To personal approach, number of word card
    public event Action<int, bool> OnToggleChanged;

    private void Awake()
    {
        if (selectToggle != null)
        {
            selectToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    public void SetData(string kanji, string meaning, float correctRate, int id)
    {
        this.kanjiTMP.text = $"【{kanji}】";
        this.meaningTMP.text = meaning;
        this.correctRateTMP.text = $"[정답률 : {correctRate:F1}%]";
        this.idTMP.text = $"ID-{id:D3}";
        this.wordId = id;

        selectToggle.isOn = false;

        OnCardSpawned?.Invoke(this, id);
    }

    public void OnToggleValueChanged(bool isOn)
    {
        OnToggleChanged?.Invoke(wordId, isOn);
    }

    private void OnDestroy()
    {
        if (selectToggle != null)
        {
            selectToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
            
        OnToggleChanged = null;
    }
}
