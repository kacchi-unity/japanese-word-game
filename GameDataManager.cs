using UnityEngine;
using System;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    [Header("런타임 가변 데이터 대상 SO Dictionary")]
    [Tooltip("해당 SO들을 인스펙터로 여기에 다 넣으세요")]
    [SerializeField] private List<ScriptableObject> saveSOList = new List<ScriptableObject>();

    private Dictionary<Type, ISaveSO> saveSODictionary = new Dictionary<Type, ISaveSO>();

    private string path;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            path = Application.persistentDataPath;

            foreach (var SO in saveSOList)
            {
                ISaveSO InterfaceSO = SO as ISaveSO;
                if (InterfaceSO != null)
                {
                    saveSODictionary[SO.GetType()] = InterfaceSO;
                }
                else
                {
                    Debug.LogWarning($"{this.name}: [{SO.name}]의 가변 인테페이스 형 변환 실패");
                }
            }

            LoadAllData();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public T GetData<T>() where T : class, ISaveSO
    {
        if (saveSODictionary.TryGetValue(typeof(T), out var InterfaceSO))
        {
            return InterfaceSO as T;
        }
        else
        {
            Debug.LogError($"{typeof(T).Name}를 DataManager에서 찾을 수 없습니다");
            return null;
        }
    }

    public void SaveAllData()
    {
        foreach (var InterfaceSO in saveSODictionary.Values)
        {
            if (InterfaceSO != null)
            {
                InterfaceSO.Save(path);
                Debug.Log($"{this.name}: [{InterfaceSO.Name}] 저장 완료");
            }
        }
        Debug.Log($"{this.name}: 모든 가변 데이터 저장 완료");
    }

    public void LoadAllData()
    {
        foreach (var InterfaceSO in saveSODictionary.Values)
        {
            if (InterfaceSO != null)
            {
                InterfaceSO.Load(path);
            }
        }
        Debug.Log($"{this.name}: 모든 가변 데이터 로드 완료");
    }

    //인게임 Sword Record 도감, EP 보상 데이터 오염 방지를 위해 포커스 자동 저장 비활성화
    // -> Scene_Result 정산 또는 상점 구매 완료 시점에 직접 호출 방식 대체
    /*void OnApplicationQuit()
    {
        SaveAllData();
    }*/

    void OnApplicationFocus(bool focus)
    {
        // 모바일에서 홈 화면으로 나갈 때 안전하게 저장
        if (!focus)
        {
            SaveAllData();
        }
    }
}
