using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordCardGenerator : MonoBehaviour
{
    public SwordRecordSO swordRecordSO;
    public WordDataBaseSO wordDataBaseSO;
    public WordCardSetting wordCardPrefab;
    public RectTransform content;
    public TextMeshProUGUI noneMessageText;

    List<int> swordRecordList = null;
    Dictionary<int, Word> wordDataBase = null;

    void Start()
    {
        noneMessageText.text = null;
        this.swordRecordList = swordRecordSO.GetSwordRecordList();
        this.wordDataBase = wordDataBaseSO.GetWordDataBase();
        
        PrintWordCards();
    }

    void PrintWordCards()
    {
        if (swordRecordList == null || wordDataBase == null)
        {
            Debug.LogWarning("List 또는 Dictinary가 할당되지 않았습니다. 로직을 중단합니다.");
            return;
        }

        if(wordDataBase.Count == 0)
        {
            Debug.LogWarning("Dictionary Elenemy 개수가 0입니다. 로직을 중단합니다.");
            return;
        }

        if (swordRecordList.Count == 0)
        {
            noneMessageText.text = "전투를 시작하여 단어를 모아보세요!";
            return;
        }

        foreach (int item in swordRecordList)
        {
            string kanji = wordDataBase[item].kanji;
            string meaning = wordDataBase[item].meaning;
            float correctRate = swordRecordSO.GetCorrectRate(item);

            WordCardSetting wordCard = Instantiate(this.wordCardPrefab, this.content);
            wordCard.SetData(kanji, meaning, correctRate, item);
        }

    }
}
