using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class WordLoader : MonoBehaviour
{
    [Header("Setting SO")]
    [SerializeField] private WordDataBaseSO wordDataBaseSO;
    [SerializeField] private SwordRecordSO swordRecordSO;
    [SerializeField] private GameSessionSO gameSessionSO;

    List<Word> allWordList;
    WordData data;

    private void OnEnable()
    {
        TitleButtonManager.OnResetButtonClick += ProcessResetButton;
    }

    private void OnDisable()
    {
        TitleButtonManager.OnResetButtonClick -= ProcessResetButton;
    }

    void Awake()
    {

#if UNITY_EDITOR
        Debug.Log("Editor mode: 데이터를 초기화 및 Json Load를 진행합니다.");
        ResetEverything();
        LoadJsonAndFillPool();
        return;
#endif
        if (PlayerPrefs.GetInt("FirstWordLoad", 0) == 0)
        {
            Debug.Log("이 기기에서 처음 실행됨! 데이터를 새로 로드합니다.");
            ResetEverything();
            LoadJsonAndFillPool();

            PlayerPrefs.SetInt("FirstWordLoad", 1);
            PlayerPrefs.Save();
        }
    }

    void ResetEverything() //Only build
    {
        wordDataBaseSO.ResetWordDataBase();
        swordRecordSO.ResetSwordRecord();
        swordRecordSO.ResetCorrectRate();
        swordRecordSO.ResetUnused();

        gameSessionSO.ResetEP();

        Debug.Log("ResetEverything(): 모든 데이터를 초기화했습니다.");
    }

    void LoadJsonAndFillPool()
    {
        //Creates path regardless of the OS to safely
        string path, json;

        path = Path.Combine(Application.streamingAssetsPath, "words.json");
        if(File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = JsonUtility.FromJson<WordData>(json);
            allWordList = data.words;

            //Initial setting in swordRecordSO
            swordRecordSO.InitializeDataset(allWordList.Count);

            //Fill DB
            wordDataBaseSO.SetWordDataBase(data.words);

        }

        Debug.Log("LoadJsonAndFillPool(): Json 소스 데이터를 로드했습니다.");
    }

    void ProcessResetButton()
    {
        ResetEverything();
        LoadJsonAndFillPool();
        ModalManager.Instance.ShowAlertModal("초기화 완료", null);
        SOSaveManager.Instance.SaveAllData();
    }
}
