using TMPro;
using UnityEngine;

public class WordCardSetting : MonoBehaviour
{
    public TextMeshProUGUI kanjiTMP;
    public TextMeshProUGUI meaningTMP;
    public TextMeshProUGUI correctRateTMP;
    public TextMeshProUGUI idTMP;

    public void SetData(string kanji, string meaning, float correctRate, int id)
    {
        this.kanjiTMP.text = $"【{kanji}】";
        this.meaningTMP.text = meaning;
        this.correctRateTMP.text = $"[정답률 : {correctRate:F1}%]";
        this.idTMP.text = $"ID-{id:D3}";
    }
}
