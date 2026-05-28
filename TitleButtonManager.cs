using UnityEngine;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    [Header ("Button Connect")]
    [Tooltip ("타이틀 씬 하단 버튼을 연결하세요.")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button sourceCreidtButton;

    [Header("Credits Data")]
    [Tooltip("크레딧 텍스트를 입력하세요.")]
    [TextArea(10, 20)]
    [SerializeField] private string creaditText;

    private void OnEnable()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnClickStartButton);
            sourceCreidtButton.onClick.AddListener(PrintSourceCredit);
        }
    }

    private void OnDisable()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(OnClickStartButton);
            sourceCreidtButton.onClick.RemoveListener(PrintSourceCredit);
        }
    }

    public void OnClickStartButton()
    {
        BaseSceneManager.Instance.ChangeSceneWithLoading(CoreSceneType.Scene_Lobby);
    }

    public void PrintSourceCredit()
    {
        string smallText = $"<size=63>{creaditText}</size>";
        ModalManager.Instance.ShowAlertModal(smallText, null);
    }
}
