using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class WordLoader : MonoBehaviour
{
    List<Word> allWordList;
    WordData data;
    public WordDataBaseSO wordDataBaseSO;
    public SwordRecordSO swordRecordSO;

    string path, json;

    void Awake()
    {

#if UNITY_EDITOR
        Debug.Log("개발자 모드: 데이터를 초기화합니다.");
        ResetEverything();
        LoadJsonAndFillPool();
        return;

#else
        if (PlayerPrefs.GetInt("FirstRun", 0) == 0)
        {
            Debug.Log("이 기기에서 처음 실행됨! 데이터를 새로 로드합니다.");
            ResetEverything();
            LoadJsonAndFillPool();

            PlayerPrefs.SetInt("FirstRun", 1);
            PlayerPrefs.Save();
        }
#endif
    }

    void ResetEverything() //Only build
    {
        wordDataBaseSO.ResetWordDataBase();
        swordRecordSO.ResetSwordRecordHash();
        swordRecordSO.ResetUnusedHash();
        Debug.Log("Editor mode: 모든 데이터를 초기화하고 새로 로드합니다.");
    }

    void LoadJsonAndFillPool()
    {
        //Creates path regardless of the OS to safely
        path = Path.Combine(Application.streamingAssetsPath, "words.json");
        if(File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = JsonUtility.FromJson<WordData>(json);
            allWordList = data.words;

            //Initial setting in swordRecordSO
            swordRecordSO.SetUnusedHash(allWordList.Count);

            //Fill DB
            wordDataBaseSO.SetWordDataBase(data.words);

        }
    }
}
