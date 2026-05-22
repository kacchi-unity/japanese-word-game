using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static event Action<Vector3> OnAnswerCorrect;

    public EnemyListSO enemyListSO;
    public TextMeshProUGUI result;
    public SwordRecordSO swordRecordSO;

    float textDuration = 1.0f;
    float fadeOutTime = 1.0f;
    private Coroutine effectCoroutine;


    private void OnEnable()
    {
        EnemyController.OnPlayerDamaged += RecordFail;
    }

    private void OnDisable()
    {
        EnemyController.OnPlayerDamaged -= RecordFail;
    }

    //check and comparing answer
    public void CheckAnswer(string playerInput)
    {
        string replacedPlayerInput = playerInput.Replace(" ", "").ToLower();

        EnemyData removeTarget = null;

        //check answer in enemy field
        foreach (EnemyData data in enemyListSO.GetEnemyList())
        {
            if (replacedPlayerInput.Equals(data.GetMeaning().Replace(" ","").ToLower()))
            {
                //give enemy position info
                Vector3 enemyPos = data.GetEnemyGameObject().transform.position;
                OnAnswerCorrect?.Invoke(enemyPos);

                //destroy only "first correct object" with EnemyController.cs
                EnemyController controller = data.GetEnemyGameObject().GetComponent<EnemyController>();
                controller.isKilledbyPlayer = true;
                controller.Die();
                
                removeTarget = data;
                break; //break foreach loop for destroy only first object
            }
        }

        //remove element from list
        if (removeTarget != null)
        {
            swordRecordSO.RecordCorrectResult(removeTarget.GetId(), true);

            enemyListSO.RemoveEnemyData(removeTarget);
            ShowAnswerEffect("정답!");
        }

        else //removeTarget is null
        {
            ShowAnswerEffect("정답이 없습니다!!");
        }
    }//CheckAsnwer(s)

    private void RecordFail(int wordId, float fadeIn_unused, float fadeOut_unused2, float damage_unused3)
    {
        swordRecordSO.RecordCorrectResult(wordId, false);
    }

    void ShowAnswerEffect(string text)
    {
        if (this.effectCoroutine != null)
        {
            StopCoroutine(this.effectCoroutine);
        }

        this.effectCoroutine = StartCoroutine(ShowAndFadeRoutine(text));
    }

    IEnumerator ShowAndFadeRoutine(string text)
    {
        result.text = text;
        result.alpha = 1.0f;
        yield return new WaitForSeconds(this.textDuration);

        float elapsed = 0f;
        while (elapsed  < this.fadeOutTime)
        {
            elapsed += Time.deltaTime;
            result.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeOutTime);
            yield return null;
        }

        result.alpha = 0f;
        this.effectCoroutine = null;

    }
}
