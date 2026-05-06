using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Object inGameScene;
    [SerializeField] private string inGameSceneName;

    private void OnValidate()
    {
        if (inGameScene != null)
        {
            inGameSceneName = inGameScene.name;
        }
    }
}
