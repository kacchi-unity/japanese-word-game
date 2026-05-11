using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusPrintManager : MonoBehaviour
{
    public TextMeshProUGUI targetTMP;
    public Bonus bonusType;

    private void OnEnable()
    {
        BonusCalculateManager.OnBonusCalculationComplete += PrintBonusText;
    }

    private void OnDisable()
    {
        BonusCalculateManager.OnBonusCalculationComplete -= PrintBonusText;
    }

    void PrintBonusText(Dictionary<Bonus, float> targetDictionary)
    {
        if (targetDictionary.ContainsKey(this.bonusType))
        {
            targetTMP.text = $"+ {targetDictionary[bonusType].ToString("P0")}";
        }

        else
        {
            targetTMP.text = "Erorr: Null";
        }
    }
}
