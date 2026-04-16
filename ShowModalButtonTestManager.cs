using UnityEngine;

public class ShowModalButtonTestManager : MonoBehaviour
{
    //alert
    public void OpenAlertModal()
    {
        ModalManager.Instance.ShowAlertModal("이것은 Alert Modal 창입니다", OkAction);
    }

    void OkAction()
    {
        Debug.Log("Alert Modal: 확인 버튼 누름 감지");
    }

    //confirm 
    public void OpenConfirmModal()
    {
        ModalManager.Instance.ShowConfirmModal("이것은 Confirm Modal 창입니다", ConfirmAction, CancleAction);
    }

    void ConfirmAction()
    {
        Debug.Log("Confirm Modal: 확인 버튼 누름 감지");
    }

    void CancleAction()
    {
        Debug.Log("Confirm Modal: 취소 버튼 누름 감지");
    }
}
