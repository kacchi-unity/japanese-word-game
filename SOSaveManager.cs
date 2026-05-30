using UnityEngine;
using System.IO;

public class SOSaveManager : MonoBehaviour
{
    [Header("관리할 SO 리스트")]
    [Tooltip("로컬 저장 및 불러오기가 필요한 대상 SO를 넣어주세요")]
    [SerializeField] private ScriptableObject[] saveSOList;

    public static SOSaveManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }

#if UNITY_EDITOR
        PlayerPrefs.DeleteKey("FirstSOSave");
        Debug.Log("Editer mode: FirstSOSave 키를 0으로 초기화했습니다.");
#endif

        if (PlayerPrefs.GetInt("FirstSOSave", 0) == 0)
        {
            Debug.Log("이 기기에서 처음 실행됨! 기존 데이터 로드를 하지 않습니다.");
            PlayerPrefs.SetInt("FirstSOSave", 1);
            PlayerPrefs.Save();
            return;
        }

        this.LoadAllData();
    }

    public void SaveAllData()
    {
        foreach (var SO in saveSOList)
        {
            string json = JsonUtility.ToJson(SO);
            string path = Path.Combine(Application.persistentDataPath, SO.name + ".json");
            File.WriteAllText(path, json);
        }

        Debug.Log($"SaveAllData: 모든 SO 데이터가 로컬에 저장되었습니다.");
    }

    public void LoadAllData()
    {
        foreach (var SO in saveSOList)
        {
            string path = Path.Combine(Application.persistentDataPath, SO.name + ".json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, SO);
            }
        }

        Debug.Log("LoadAllData: 모든 SO 데이터를 로드했습니다.");
    }

    //Auto save when game quit
    private void OnApplicationQuit()
    {
        SaveAllData();
    }

    //Mobile save when get home title
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveAllData();
        }
    }
}
