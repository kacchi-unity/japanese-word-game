using UnityEngine;

public class SoundOptionButton : MonoBehaviour
{

    public void OpenSoundPopup()
    {
        PopupManager.Instance.ShowPopup("이것은 소리 설정 창입니다", ClickConfirmAction);
    }

    void ClickConfirmAction()
    {
        Debug.Log("확인 또는 닫기 버튼 누를시 작동할 함수");
    }
}
