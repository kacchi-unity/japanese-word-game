using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyButtonManager : MonoBehaviour
{
    public static event Action OnStartButtonClick;
    public string inGameSceneName = "Scene_InGame";
    private SceneTracker.SceneType nextScene;

    public LobbySettingSO lobbySetting; //test!

    void OnEnable()
    {
        SwordRecordSliderBarManager.isSwordRecordSliderActive += SetNextScene;
    }

    void OnDisable()
    {
        SwordRecordSliderBarManager.isSwordRecordSliderActive -= SetNextScene;
    }

    void SetNextScene(bool isswordRecordSliderActive)
    {
        nextScene = isswordRecordSliderActive ? SceneTracker.SceneType.SwordRecord : SceneTracker.SceneType.Lobby;
    }

    public void ClickStartButton()
    {
        StartCoroutine(StartProcess());
        
    }

    public void ClickSwordRecordButton()
    {
        SceneManager.LoadScene("Scene_SwordRecord");
    }

    IEnumerator StartProcess()
    {
        OnStartButtonClick?.Invoke();
        yield return new WaitForSeconds(0.5f);
        ModalManager.Instance.ShowConfirmModal("게임을 시작하시겠습니까?",
            () =>
            { SceneTracker.previousScene = nextScene;
                SceneManager.LoadScene(inGameSceneName);
            } , null);
    }
}
