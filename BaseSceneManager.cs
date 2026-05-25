using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public enum CoreSceneType
{
    None,
    Scene_Loading,
    Scene_Title,
    Scene_Lobby,
    Scene_InGame,
    Scene_Result,
    Scene_SwordRecord
}

public class BaseSceneManager : MonoBehaviour
{
    //SingleTon
    public static BaseSceneManager Instance;

    [Serializable]
    private struct ScenePair
    {
        [Tooltip ("CoreSceneType을 선택하세요")]
        public CoreSceneType sceneType;
        [Tooltip("AssetReference를 연결하세요")]
        public AssetReference sceneReference;
    }

    [SerializeField] private List<ScenePair> scenePairList;

    private Dictionary<CoreSceneType, AssetReference> sceneDictionary = new Dictionary<CoreSceneType, AssetReference>();

    public CoreSceneType targetScene { get; private set; } = CoreSceneType.None;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Addressables.InitializeAsync();
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var pair in scenePairList)
        {
            if (!sceneDictionary.ContainsKey(pair.sceneType))
            {
                sceneDictionary.Add(pair.sceneType, pair.sceneReference);
            }
        }
    }

    //using at loading scene script
    public AssetReference GetTargetSceneReference()
    {
        if (this.sceneDictionary.TryGetValue(this.targetScene, out AssetReference targetSceneReference))
        {
            return targetSceneReference;
        }
        return null;
    }

    //using to convert loading scene
    public void ChangeScene(CoreSceneType nextScene)
    {
        if (this.sceneDictionary.TryGetValue(nextScene, out AssetReference nextSceneReference))
        {
            Addressables.LoadSceneAsync(nextSceneReference, LoadSceneMode.Single);
        }

        else
        {
            Debug.LogWarning($"sceneDictionary 내 CoreSceneType {nextScene}가 존재하지 않습니다.");
        }
    }

    public void ChangeSceneWithLoading(CoreSceneType nextScene)
    {
        if (nextScene.Equals(CoreSceneType.Scene_Loading))
        {
            Debug.LogWarning($"무한루프 위험: BaseSceneManager 내 ChangeSceneWithLoading의 목적지로 {nextScene}을 설정할 수 없습니다. 실행을 취소합니다.");
            return;
        }
        this.targetScene = nextScene;
        this.ChangeScene(CoreSceneType.Scene_Loading);
    }
}