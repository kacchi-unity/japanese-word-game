using TMPro;
using UnityEngine;

public class EnemyDeathScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetScore(int amount)
    {
        text.text = "+" + amount.ToString();
    }
}