using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPrintManager : MonoBehaviour
{
    public TextMeshProUGUI targetTMP;
    public Result resultType;

    private void OnEnable()
    {
        BonusCalculateManager.OnResultCalculationComplete += PrintBonusText;
    }

    private void OnDisable()
    {
        BonusCalculateManager.OnResultCalculationComplete -= PrintBonusText;
    }

    void PrintBonusText(Dictionary<Result, float> targetDictionary)
    {
        if (targetDictionary.ContainsKey(this.resultType))
        {
            if (this.resultType == Result.Reward)
            {
                float rewardPercent = 100f + targetDictionary[Result.Total] * 100f;
                targetTMP.text = $"{targetDictionary[Result.Score]}점 × {rewardPercent:F0}% = " +
                    $"<color=#E84A4A><b><size=120%>{targetDictionary[Result.Reward]}EP</size></b></color>";
            }
            else if(this.resultType == Result.Total)
            {
                targetTMP.text = $"+ {targetDictionary[resultType]:P0}";
            }
        }
        else
        {
            targetTMP.text = "Erorr: Null";
        }
    }
}
