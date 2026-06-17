using TMPro;
using UnityEngine;
using System.Collections;

public class EPCountManager : MonoBehaviour
{
    [Header ("UI Setting")]
    [SerializeField] private TextMeshProUGUI totalEPText;
    [SerializeField] private TextMeshProUGUI addAmountEPText;

    [Header ("EP 증가 카운트 애니메이션 시간")]
    [SerializeField] private float countAnimationDuration = 3.0f;

    [Header("추가 EP 텍스트 페이드아웃 애니메이션 시간")]
    [SerializeField] private float addAmountTextFadeOutDuration = 2.0f;

    void Start()
    {
        int totalEP = GameDataManager.Instance.GetData<GameSessionSO>().EnlightenmentPoint;
        int addAmountEP = GameDataManager.Instance.GetData<GameSessionSO>().AddAmountEP;

        if (addAmountEP != 0)
        {
            totalEPText.alpha = 1;
            addAmountEPText.alpha = 1;

            int previousEP = totalEP - addAmountEP;

            totalEPText.text = $"<color=#D32F2F><b>{previousEP}</b></color>";
            addAmountEPText.text = $"<color=#FF4105><b>+ ({addAmountEP})</b></color>";

            GameDataManager.Instance.GetData<GameSessionSO>().ResetAddAmountEP();
            GameDataManager.Instance.SaveAllData();
            StartCoroutine(AnimateEPCount(previousEP, totalEP));
            StartCoroutine(FadeOutText(addAmountEPText));

        }
        else //addAmountEp == 0
        {
            totalEPText.text = $"<color=#000000>{totalEP}</color>";
            addAmountEPText.alpha = 0;
        }
    }

    IEnumerator AnimateEPCount(int startEP, int endEP)
    {
        float currentTime = 0f;
        int currentEP;

        while (currentTime < countAnimationDuration)
        {
            currentTime += Time.deltaTime;

            currentEP = (int)Mathf.Lerp(startEP, endEP, currentTime / countAnimationDuration);

            totalEPText.text = $"<color=#D32F2F><b>{currentEP}</b></color>";
            yield return null;
        }

        totalEPText.text = $"<color=#000000>{endEP}</color>"; // 최종 값 보정
    }

    IEnumerator FadeOutText(TextMeshProUGUI targetTMP)
    {
        float currentTime = 0f;

        while (currentTime < addAmountTextFadeOutDuration)
        {
            currentTime += Time.deltaTime;
            targetTMP.alpha = Mathf.Lerp(1, 0, currentTime / addAmountTextFadeOutDuration);
            yield return null;
        }

        targetTMP.alpha = 0;
    }

}
