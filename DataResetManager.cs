using UnityEngine;

public class DataResetManager : MonoBehaviour
{
    [Header("Setting SO")]
    [SerializeField] private SwordRecordSO swordRecordSO;

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

        swordRecordSO.ResetSwordRecord();
        swordRecordSO.ResetCorrectRate();
        swordRecordSO.ResetUnused();

        GameDataManager.Instance.GetData<GameSessionSO>().ResetEP();

        ModalManager.Instance.ShowAlertModal("초기화 완료", null);

        Debug.Log("ResetEverything(): 모든 데이터를 초기화했습니다.");
    }
}
