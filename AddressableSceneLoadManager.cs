using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddressableSceneLoadManager : MonoBehaviour
{
    public Slider loadingBar;
    public TextMeshProUGUI LoadingMessage;

    [Header ("로딩 메시지 입력")]
    [Tooltip ("로딩 완료 전 메시지를 입력하세요")]
    [SerializeField] private string messageBeforeLoadingDone = null;
    /*[Tooltip("로딩 완료 후 메시지를 입력하세요")]
    [SerializeField] private string messageAfterLoadingDone = null;

    [Header("Settings")]
    [Tooltip("최소 로딩 연출 시간 [초]")]
    [SerializeField] private float minLoadingTime = 1.0f;
    [Tooltip("로딩 완료 시 씬 변환 전 대기 시간")]
    [SerializeField] private float waitBeforeLoadSceneTime = 0.5f;*/


    void Start()
    {
        StartCoroutine(LoadTargetScene());
    }

    IEnumerator LoadTargetScene()
    {
        AssetReference targetSceneReference = BaseSceneManager.Instance.GetTargetSceneReference();

        if (targetSceneReference == null)
        {
            Debug.LogWarning($"BaseSceneManager 내 targetSceneReference가 존재하지 않습니다.");
            yield break;
        }

        loadingBar.value = 0;

        this.LoadingMessage.text = messageBeforeLoadingDone;

        var handle = Addressables.LoadSceneAsync(targetSceneReference, LoadSceneMode.Single, true);

        while (handle.IsDone)
        {
            loadingBar.value = handle.PercentComplete;
        }
    }
}
