using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class TitleButtonManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void OnEnable()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnClickStartButton);
        }
    }

    private void OnDisable()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(OnClickStartButton);
        }
    }

    public void OnClickStartButton()
    {
        BaseSceneManager.Instance.ChangeSceneWithLoading(CoreSceneType.Scene_Lobby);
    }
}
