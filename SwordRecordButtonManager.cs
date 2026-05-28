using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordRecordButtonManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noneMessageText;
    [SerializeField] private SelectWordProcessManager selectWordProcessManager;
    [SerializeField] private Button LobbyButton;
    [SerializeField] private GameSessionSO gameSessionSO;

    private int minSelectCount = 1;
    private int maxSelectCount;

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

    public void Start()
    {
        this.maxSelectCount = gameSessionSO.SystemPlayWordLimitCount;
    }

    public void ProcessStartButton(int selectSwordRecordHashCount)
    {
        if (selectSwordRecordHashCount < minSelectCount)
        {
            ModalManager.Instance.ShowAlertModal($"단어를 {minSelectCount}개 이상 선택해야 합니다.", null);
        }
        else if (selectSwordRecordHashCount <= maxSelectCount)
        {
            ModalManager.Instance.ShowConfirmModal(
                $"선택한 {selectSwordRecordHashCount}개 단어로\n복습 게임을 시작하시겠습니까?\n(*리워드 획득 가능)",
                () => BaseSceneManager.Instance.ChangeScene(CoreSceneType.Scene_Lobby),
                null
            );
        }
        else
        {
            ModalManager.Instance.ShowAlertModal($"단어는 최대 {maxSelectCount}개 까지\n선택 가능합니다.", null);
        }
    }

    public void ClickLobbyButton()
    {
        if (noneMessageText.text != null)
        {
            noneMessageText.alpha = 0f;
        }
        ModalManager.Instance.ShowConfirmModal("로비로 돌아가시겠습니까?",
            () => BaseSceneManager.Instance.ChangeScene(CoreSceneType.Scene_Lobby),
            () => noneMessageText.alpha = 1f);
    }
}
