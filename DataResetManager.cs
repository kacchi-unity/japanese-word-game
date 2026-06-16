using UnityEngine;

public class DataResetManager : MonoBehaviour
{
    private void OnEnable()
    {
        TitleButtonManager.OnResetButtonClick += ResetEveryData;
    }

    private void OnDisable()
    {
        TitleButtonManager.OnResetButtonClick -= ResetEveryData;
    }

    void ResetEveryData()
    {
        WordLoader.Instance.ResetWordDataBase();

        GameDataManager.Instance.GetData<GameSessionSO>().ResetEP();

        GameDataManager.Instance.SaveAllData();

        ModalManager.Instance.ShowAlertModal("초기화 완료", null);

        Debug.Log("ResetEverything(): 모든 데이터를 초기화했습니다.");
    }
}
