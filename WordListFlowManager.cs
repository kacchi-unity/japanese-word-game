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

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    void Start()
    {
        selectedWordIndex.Clear();

        selectedWordList.Clear();

        int selectAmount = (int)lobbySetting.settingValue.GetValue(SettingList.WordCount);

        this.selectedWordIndex = swordRecord.GetRandomId(selectAmount);

        foreach (int item in this.selectedWordIndex)
        {
            this.selectedWordList.Add(wordDataBaseSO.GetWordDataBase()[item]);
        }

        foreach(var item in this.selectedWordList )
        {
            Debug.Log($"{item.id} = {item.kanji}, {item.meaning}");
        } //test

        enemyGenerator.SetSelectedWordList(this.selectedWordList);

    }
}
