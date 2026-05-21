using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordCardSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kanjiTMP;
    [SerializeField] private TextMeshProUGUI meaningTMP;
    [SerializeField] private TextMeshProUGUI correctRateTMP;
    [SerializeField] private TextMeshProUGUI idTMP;
    [SerializeField] private Toggle selectToggle;
    [SerializeField] private RectTransform rectTransform;

    //Get
    public TextMeshProUGUI KanjiTMP => kanjiTMP;
    public TextMeshProUGUI MeaningTMP => meaningTMP;
    public TextMeshProUGUI CorrectRateTMP => correctRateTMP;
    public TextMeshProUGUI IdTMP => idTMP;
    public Toggle SelectToggle => selectToggle;
    public RectTransform RectTransform => rectTransform;

    //Value
    [SerializeField] private int wordId;
    public int WordId => wordId;

    //To find this object, only one
    public static event Action<WordCardSetting, int> OnCardSpawned;

    //To personal approach, number of word card
    public event Action<int, bool> OnToggleChanged;

    private void OnEnable()
    {
        if (selectToggle != null)
        {
            selectToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    public void SetData(string kanji, string meaning, float correctRate, int id, bool isSelect)
    {
        this.kanjiTMP.text = $"【{kanji}】";
        this.meaningTMP.text = meaning;
        this.correctRateTMP.text = $"[정답률 : {correctRate:F1}%]";
        this.idTMP.text = $"ID-{id:D3}";
        this.wordId = id;

        selectToggle.onValueChanged.RemoveListener(OnToggleValueChanged);

        selectToggle.isOn = isSelect;

        selectToggle.onValueChanged.AddListener(OnToggleValueChanged);

        OnCardSpawned?.Invoke(this, id);
    }

    public void OnToggleValueChanged(bool isOn)
    {
        OnToggleChanged?.Invoke(wordId, isOn);
    }

    private void OnDisable()
    {
        if (selectToggle != null)
        {
            selectToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
            
        OnToggleChanged = null;
    }
}
