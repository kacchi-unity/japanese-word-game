using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    [Header("런타임 사용 SO 리스트")]
    [Tooltip("런타임 가변 데이터 대상 SO를 넣어주세요")]
    [SerializeField] private LobbySettingSO lobbySettingSO;

    [Header("현재 런타임 세팅 (디버그용)")]
    [SerializeField] private RuntimeLobbySetting runtimeLobbySetting;
    public RuntimeLobbySetting RuntimeLobbySetting => runtimeLobbySetting;


    private string path;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            path = Path.Combine(Application.persistentDataPath, "GameDataManager" + ".json");

            SetData();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void SetData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            runtimeLobbySetting = JsonUtility.FromJson<RuntimeLobbySetting>(json);
            Debug.Log("GameDataManager: 기존 세이브 파일로부터 가변 데이터 로드 완료.");
        }

        else
        {
            runtimeLobbySetting = new RuntimeLobbySetting();

            LobbySettingSO.DefaultLobbySetting deafaultSetting = lobbySettingSO.defaultLobbySetting;

            runtimeLobbySetting.SetValue(SettingList.WordCount, deafaultSetting.wordCount);
            runtimeLobbySetting.SetValue(SettingList.PlayerHp, deafaultSetting.playerHp);
            runtimeLobbySetting.SetValue(SettingList.TimeLimit, deafaultSetting.timeLimit);
            runtimeLobbySetting.SetValue(SettingList.EnemySpeedRate, deafaultSetting.enemySpeedRate);
            runtimeLobbySetting.SetValue(SettingList.HintActiveTime, deafaultSetting.hintActiveTime);
            runtimeLobbySetting.SetValue(SettingList.EnemySpawnDelay, deafaultSetting.enemySpawnDelay);

            Debug.Log("GameDataManager: 세이브 파일이 존재하지 않아 SO 원본 데이터로부터 초기값 생성.");
            SaveData(); //test
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(RuntimeLobbySetting, true);
        File.WriteAllText(path, json);
        Debug.Log($"GameDataManager: 가변 데이터 저장 완료. 경로: {path}");
    }
}
