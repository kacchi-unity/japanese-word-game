using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusPrintManager : MonoBehaviour
{
    public TextMeshProUGUI targetTMP;
    public Bonus bonusType;
    public int score = 50;

    private void OnEnable()
    {
        BonusCalculateManager.OnCalculationComplete += PrintBonusText;
    }

    private void OnDisable()
    {
        BonusCalculateManager.OnCalculationComplete -= PrintBonusText;
    }

    void PrintBonusText(Dictionary<Bonus, float> targetDictionary)
    {
        if (targetDictionary.ContainsKey(this.bonusType))
        {
            if (this.bonusType == Bonus.Reward)
            {
                float rewardPercent = 100f + targetDictionary[Bonus.Total] * 100f;
                targetTMP.text = $"{this.score}점 × {rewardPercent}% = {targetDictionary[Bonus.Reward]}EP";
            }
            else
            {
                targetTMP.text = $"+ {targetDictionary[bonusType].ToString("P0")}";
            }
        }
        else
        {
            targetTMP.text = "Erorr: Null";
        }
    }
}
