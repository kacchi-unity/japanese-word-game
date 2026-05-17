using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordRecordButtonManager : MonoBehaviour
{
    public TextMeshProUGUI noneMessageText;
    public SelectWordProcessManager selectWordProcessManager;

    public void ClickStartButton()
    {
        ModalManager.Instance.ShowConfirmModal("선택한 단어로\n게임을 시작하시겠습니까?",
            ()=> selectWordProcessManager.OnStartButtonClick(), null);
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
