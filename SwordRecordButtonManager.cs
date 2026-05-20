using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordRecordButtonManager : MonoBehaviour
{
    public TextMeshProUGUI noneMessageText;
    public SelectWordProcessManager selectWordProcessManager;
    public Button LobbyButton;

    private void OnEnable()
    {
        SelectWordProcessManager.isClickStartButton += ProcessStartButton;
    }
    private void OnDisable()
    {
        SelectWordProcessManager.isClickStartButton -= ProcessStartButton;
    }
    private void Awake()
    {
        LobbyButton.onClick.AddListener(ClickLobbyButton);
    }

    private void OnDestroy()
    {
        LobbyButton.onClick.RemoveListener(ClickLobbyButton);
    }

    public void ProcessStartButton(int selectSwordRecordHashCount)
    {
        if (selectSwordRecordHashCount <= 0)
        {
            ModalManager.Instance.ShowAlertModal("단어를 1개 이상 선택해야합니다." , null);
        }
        else
        {
            ModalManager.Instance.ShowConfirmModal($"선택한 {selectSwordRecordHashCount}개 단어로\n복습 게임을 시작하시겠습니까?\n(*리워드 획득 가능)",
            () => SceneManager.LoadScene("Scene_Lobby"), null);
        }
        
    }

    public void ClickLobbyButton()
    {
        if (noneMessageText.text != null)
        {
            noneMessageText.alpha = 0f;
        }
        ModalManager.Instance.ShowConfirmModal("로비로 돌아가시겠습니까?", () => SceneManager.LoadScene("Scene_Lobby"),
            () => noneMessageText.alpha = 1f);
    }
}
