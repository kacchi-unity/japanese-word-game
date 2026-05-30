using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    [Header ("Button Connect")]
    [Tooltip ("타이틀 씬 하단 버튼을 연결하세요.")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button sourceCreidtButton;
    [SerializeField] private Button resetButton;

    [Header("Credits Data")]
    [Tooltip("크레딧 텍스트를 입력하세요.")]
    [TextArea(10, 20)]
    [SerializeField] private string creaditText;

    [Header ("Text Setting")]
    [Tooltip("초기화 버튼 창 텍스트를 입력하세요.")]
    [TextArea(2,2)]
    [SerializeField] private string resetText;

    public static event Action OnResetButtonClick;

    private void OnEnable()
    {
        if (startButton != null) startButton.onClick.AddListener(OnClickStartButton);
        if (sourceCreidtButton != null) sourceCreidtButton.onClick.AddListener(PrintSourceCredit);
        if (resetButton != null) resetButton.onClick.AddListener(ConfilmAndProcessReset);
    }

    private void OnDisable()
    {
        if (startButton != null) startButton.onClick.RemoveListener(OnClickStartButton);
        if (sourceCreidtButton != null) sourceCreidtButton.onClick.RemoveListener(PrintSourceCredit);
        if (resetButton != null) resetButton.onClick.RemoveListener(ConfilmAndProcessReset);
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

    public void ConfilmAndProcessReset()
    {
        ModalManager.Instance.ShowConfirmModal(
            resetText,
            ()=> { OnResetButtonClick?.Invoke(); },
            null
            );
    }
}
