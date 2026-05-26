using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyButtonManager : MonoBehaviour
{
    public static event Action OnStartButtonClick;
    public string inGameSceneName = "Scene_InGame";
    private SceneTracker.SceneType nextScene;

    [SerializeField] private Button toInGameButton;
    [SerializeField] private Button toSwordRecordButton;


    void OnEnable()
    {
        SwordRecordSliderBarManager.isSwordRecordSliderActive += SetNextScene;
        toInGameButton.onClick.AddListener(ClickToInGameButton);
        toSwordRecordButton.onClick.AddListener(ClickToSwordRecordButton);
    }

    void OnDisable()
    {
        SwordRecordSliderBarManager.isSwordRecordSliderActive -= SetNextScene;
        toInGameButton.onClick.RemoveListener(ClickToInGameButton);
        toSwordRecordButton.onClick.RemoveListener(ClickToSwordRecordButton);
    }

    void SetNextScene(bool isswordRecordSliderActive)
    {
        nextScene = isswordRecordSliderActive ? SceneTracker.SceneType.SwordRecord : SceneTracker.SceneType.Lobby;
    }

    public void ClickToInGameButton()
    {
        StartCoroutine(StartProcess());
        
    }

    public void ClickToSwordRecordButton()
    {
        BaseSceneManager.Instance.ChangeScene(CoreSceneType.Scene_SwordRecord);
    }

    IEnumerator StartProcess()
    {
        OnStartButtonClick?.Invoke();

        ModalManager.Instance.ShowConfirmModal(
            "게임을 시작하시겠습니까?",
            () => {
                SceneTracker.previousScene = nextScene;
                BaseSceneManager.Instance.ChangeSceneWithLoading(CoreSceneType.Scene_InGame);
            },
            null
            );

        yield return null;
    }
}
