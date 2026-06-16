using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordLoader : MonoBehaviour
{
    [SerializeField] private SwordRecordSO swordRecordSO;

    public static WordLoader Instance { get; private set; }
    WordData data;
    private string savePath, sourcePath;
    private Dictionary<int, Word> wordDataBaseDic = new Dictionary<int, Word>();

    [Serializable]
    public class RuntimeData
    {
        public List<Word> wordDataBaseList = new List<Word>();
    }

    public RuntimeData runtimeData = new RuntimeData();

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, $"{this.name}.json");
        sourcePath = Path.Combine(Application.streamingAssetsPath, "words.json");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CheckFirstRunGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CheckFirstRunGame()
    {
        if (PlayerPrefs.GetInt("FirstWordLoad", 0) == 0)
        {
            Debug.Log("이 기기에서 처음 실행됨! 데이터를 새로 로드합니다.");
            LoadJsonAndFillPool();
            this.Save();

            PlayerPrefs.SetInt("FirstWordLoad", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Load();
        }
    }

    void LoadJsonAndFillPool()
    {
        //Creates path regardless of the OS to safely
        string json;
        if(File.Exists(sourcePath))
        {
            json = File.ReadAllText(sourcePath);
            data = JsonUtility.FromJson<WordData>(json);
            runtimeData.wordDataBaseList = data.words;

            //Initial setting in swordRecordSO
            swordRecordSO.InitializeDataset(runtimeData.wordDataBaseList.Count);

            //Fill Dicionary
            FillDictionaryFromList();
            Debug.Log("Json 소스 데이터를 로드했습니다.");
        }

        
    }

    void FillDictionaryFromList()
    {
        this.wordDataBaseDic.Clear();

        if (runtimeData.wordDataBaseList == null)
        {
            return;
        }

        foreach (var item in runtimeData.wordDataBaseList)
        {
            if (!this.wordDataBaseDic.ContainsKey(item.id))
            {
                this.wordDataBaseDic.Add(item.id, item);
            }
            else
            {
                Debug.LogError($"[WordDataBaseSO] 중복된 ID 발견! ID: {item.id}, 단어명: {item.meaning}. 이 데이터는 스킵됩니다.");
            }
        }
    }

    public void Save()
    {
        SyncList();
        string json = JsonUtility.ToJson(this.runtimeData, true);
        File.WriteAllText(savePath, json);
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            runtimeData = JsonUtility.FromJson<RuntimeData>(json);
            FillDictionaryFromList();
        }
        else
        {
            LoadJsonAndFillPool();
            Debug.Log($"경로 내 파일을 찾을 수 없습니다. Json 소스 데이터를 받습니다. 경로: {savePath}");
        }
    }
    private void ValidateDic()
    {
        if (this.wordDataBaseDic.Count == 0 && runtimeData.wordDataBaseList.Count > 0)
        {
            wordDataBaseDic.Clear();
            foreach (var item in runtimeData.wordDataBaseList)
            {
                this.wordDataBaseDic[item.id] = item;
            }
        }
    }

    private void SyncList()
    {
        runtimeData.wordDataBaseList.Clear();
        foreach (var item in this.wordDataBaseDic)
        {
            runtimeData.wordDataBaseList.Add(item.Value);
        }
    }

    public Dictionary<int, Word> GetWordDataBase()
    {
        ValidateDic();
        return this.wordDataBaseDic;
    }

    public void ResetWordDataBase()
    {
        this.wordDataBaseDic.Clear();
        runtimeData.wordDataBaseList.Clear();
        LoadJsonAndFillPool();

        this.Save();
    }

    public void Test()
    {
        ValidateDic();
        Debug.Log(this.wordDataBaseDic.Count);
        Debug.Log(runtimeData.wordDataBaseList.Count);
    }

}
