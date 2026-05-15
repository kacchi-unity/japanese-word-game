using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyButtonManager : MonoBehaviour
{
    public static event Action OnStartButtonClick;
    public string inGameSceneName = "Scene_InGame";

    public LobbySettingSO lobbySetting; //test!

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
        ModalManager.Instance.ShowConfirmModal("게임을 시작하시겠습니까?",()=>SceneManager.LoadScene(inGameSceneName), null);
    }
}
