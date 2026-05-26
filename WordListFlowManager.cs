using System;
using System.Collections.Generic;
using UnityEngine;

public class WordListFlowManager : MonoBehaviour
{
    public EnemyGenerator enemyGenerator;
    public LobbySettingSO lobbySettingSO;
    public SwordRecordSO swordRecordSO;
    public WordDataBaseSO wordDataBaseSO;

    List<int> selectedWordIndex = new List<int>();
    List<Word> selectedWordList = new List<Word>();

    public static event Action<List<int>> OnSelectedWordListGenerated;

    void Start()
    {
        if (SceneTracker.previousScene.Equals(SceneTracker.SceneType.Lobby))
        {
            int selectAmount = (int)lobbySettingSO.settingValue.GetValue(SettingList.WordCount);

            this.selectedWordIndex = swordRecordSO.GetRandomId(selectAmount);

        }

        else if (SceneTracker.previousScene.Equals(SceneTracker.SceneType.SwordRecord))
        {
            if (SceneTracker.selectorSwordRecordWordList == null || SceneTracker.selectorSwordRecordWordList.Count == 0)
            {
                Debug.LogWarning("오류: selector List 확인 불가. 로직을 종료합니다.");
                return;
            }

            this.selectedWordIndex = new List<int>(SceneTracker.selectorSwordRecordWordList);

            //Correction for reward calculation
            lobbySettingSO.settingValue.SetValue(SettingList.WordCount, SceneTracker.selectorSwordRecordWordList.Count);
        }

        else
        {
            Debug.LogWarning("오류: 이전 씬 정보 확인 불가. 로직을 종료합니다.");
            return;
        }

        //Check index list (word id)
        if (selectedWordIndex != null && selectedWordIndex.Count > 0)
        {
            OnSelectedWordListGenerated?.Invoke(selectedWordIndex);
        }

        else
        {
            Debug.LogError("에러: 리스트 selectedWordIndex 확인 불가. 로직을 종료합니다.");
            return;
        }

        //Generate word list with word id list
        foreach (int item in this.selectedWordIndex)
        {
            this.selectedWordList.Add(wordDataBaseSO.GetWordDataBase()[item]);
        }

        //Check word list
        if (selectedWordList != null && selectedWordList.Count > 0)
        {
            //Generate enemy prefab and log test
            enemyGenerator.SetSelectedWordList(this.selectedWordList);

            foreach (var item in this.selectedWordList)
            {
                Debug.Log($"{item.id} = {item.kanji}, {item.meaning}");
            }
        }

        else
        {
            Debug.LogError("에러: 리스트 selectedWordList 확인 불가. 로직을 종료합니다.");
            return;
        }
    }
}
