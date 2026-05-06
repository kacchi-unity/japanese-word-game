using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public static event Action OnStartButtonClick;
    public string inGameSceneName = "Scene_InGame";

    public LobbySettingSO lobbySetting; //test!

    public void ClickStartButton()
    {
        StartCoroutine(StartProcess());
        
    }

    IEnumerator StartProcess()
    {
        OnStartButtonClick?.Invoke();
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(inGameSceneName);
    }
}
