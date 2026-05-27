using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class WordBoardSettingManager : MonoBehaviour
{
    [Header ("SO Data")]
    [SerializeField] private GameSessionSO gameSessionSO;
    [SerializeField] private WordDataBaseSO wordDataBaseSO;

    [Header ("UI Setting")]
    [SerializeField] private TextMeshProUGUI wordBoardTextPrefab;
    [SerializeField] private RectTransform boardBoxRectTransform;
    [SerializeField] private CanvasGroup wordBoardCanvasGroup;

    private void OnEnable()
    {
        WordListFlowManager.OnSelectedWordListGenerated += CreateWordText;
    }

    private void OnDisable()
    {
        WordListFlowManager.OnSelectedWordListGenerated -= CreateWordText;
    }

    void Start()
    {
        wordBoardCanvasGroup.gameObject.SetActive(true);
        wordBoardCanvasGroup.alpha = 1.0f;
    }

    void CreateWordText(List<int> wordIdList)
    {
        foreach (Transform child in this.boardBoxRectTransform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int maxWordCount = gameSessionSO.SystemPlayWordLimitCount;
        foreach(int wordId in wordIdList)
        {
            TextMeshProUGUI wordBoardText = Instantiate(wordBoardTextPrefab, boardBoxRectTransform);
            if (wordDataBaseSO.GetWordDataBase().TryGetValue(wordId, out var wordData))
            {
                wordBoardText.text = $"{wordData.kanji} [{wordData.meaning}]";
            }
            
        }
    }
}
