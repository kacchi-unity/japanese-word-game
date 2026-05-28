using TMPro;
using UnityEngine;

public class SelectCountFeedbackManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordCountText;
    [SerializeField] private GameSessionSO gameSessionSO;

    int maxCount;

    void Start()
    {
        PrintWordCountText(0);
        this.maxCount = gameSessionSO.SystemPlayWordLimitCount;
    }

    public void PrintWordCountText(int wordCount)
    {
        if (wordCount <= maxCount)
        {
            wordCountText.text = $"[선택된 단어: {wordCount,2}개]";
        }
        else
        {
            wordCountText.text = $"[선택된 단어: <color=#FF0000>{wordCount,2}</color>개]";
        }
            
    }
}
