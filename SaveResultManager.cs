using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveResultManager : MonoBehaviour
{
    [Header ("Setting UI")]
    [SerializeField] private TextMeshProUGUI saveAlertTMP;
    [SerializeField] private Button confirmButton;

    [Header("Setting Save Text")]
    [SerializeField] string beforeSaveDoneText;
    [SerializeField] string afterSaveDoneText;

    private void OnEnable()
    {
        BonusCalculateManager.OnResultCalculationComplete += SaveResult;

        if (confirmButton != null)
        {
            confirmButton.interactable = false;
        }

        if (saveAlertTMP != null)
        {
            saveAlertTMP.text = beforeSaveDoneText;
        }
    }

    private void OnDisable()
    {
        BonusCalculateManager.OnResultCalculationComplete -= SaveResult;
    }

    void SaveResult(Dictionary<Result, float> targetDictionary)
    {
        if (targetDictionary.TryGetValue(Result.Reward, out float rewardValue))
        {
            GameDataManager.Instance.GetData<GameSessionSO>().AddEP((int)rewardValue);

            GameDataManager.Instance.SaveAllData();

            if (saveAlertTMP != null)
            {
                saveAlertTMP.text = afterSaveDoneText;
            }

            if (confirmButton != null)
            {
                confirmButton.interactable = true;
            }
        }
        return;
    }
}
