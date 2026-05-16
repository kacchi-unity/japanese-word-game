using System.Collections.Generic;
using UnityEngine;

public class WordListFlowManager : MonoBehaviour
{
    public EnemyGenerator enemyGenerator;
    public LobbySettingSO lobbySetting;
    public SwordRecordSO swordRecord;
    public WordDataBaseSO wordDataBaseSO;

    List<int> selectedWordIndex = new List<int>();
    List<Word> selectedWordList = new List<Word>();

    void Start()
    {
        if (SceneTracker.previousScene.Equals(SceneTracker.SceneType.Lobby))
        {
            int selectAmount = (int)lobbySetting.settingValue.GetValue(SettingList.WordCount);

            this.selectedWordIndex = swordRecord.GetRandomId(selectAmount);

        }

        else if (SceneTracker.previousScene.Equals(SceneTracker.SceneType.SwordRecord))
        {
            if (SceneTracker.selectorSwordRecordWordList == null || SceneTracker.selectorSwordRecordWordList.Count == 0)
            {
                Debug.LogWarning("오류: selector List 확인 불가. 로직을 종료합니다.");
                return;
            }

            this.selectedWordIndex = new List<int>(SceneTracker.selectorSwordRecordWordList);
        }

        else
        {
            Debug.LogWarning("오류: 이전 씬 정보 확인 불가. 로직을 종료합니다.");
            return;
        }

        foreach (int item in this.selectedWordIndex)
        {
            this.selectedWordList.Add(wordDataBaseSO.GetWordDataBase()[item]);
        }

        //Generate enemy prefab and log test
        enemyGenerator.SetSelectedWordList(this.selectedWordList);

        foreach (var item in this.selectedWordList )
        {
            Debug.Log($"{item.id} = {item.kanji}, {item.meaning}");
        }

    }
}
