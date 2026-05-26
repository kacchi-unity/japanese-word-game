using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultButtonManager : MonoBehaviour
{
    [SerializeField] private Button resultConfilmButton;

    void OnEnable()
    {
        resultConfilmButton.onClick.AddListener(OnClickToLobbyButton);
    }

    void OnDisable()
    {
        resultConfilmButton.onClick.RemoveListener(OnClickToLobbyButton);
    }

    public void OnClickToLobbyButton()
    {
        BaseSceneManager.Instance.ChangeScene(CoreSceneType.Scene_Lobby);
    }
}