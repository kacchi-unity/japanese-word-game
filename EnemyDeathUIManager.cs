using System.Collections;
using UnityEngine;

public class EnemyDeathUIManager : MonoBehaviour
{
    public GameObject scoreUIPrefab;
    public Transform canvasTransform;

    int score = 1;

    float duration = 0.5f;
    float moveSpeed = 100f;

    private void OnEnable()
    {
        EnemyListManager.OnAnswerCorrect += DisplayDeathUI;
    }

    private void OnDisable()
    {
        EnemyListManager.OnAnswerCorrect -= DisplayDeathUI;
    }

    void DisplayDeathUI(Vector3 enemyPos)
    {
        StartCoroutine(DisplayScore(enemyPos));
    }

    IEnumerator DisplayScore(Vector3 enemyPos)
    {
        GameObject scorePrefab = Instantiate(scoreUIPrefab, canvasTransform);
        scorePrefab.transform.SetParent(canvasTransform, false);

        scorePrefab.GetComponent<EnemyDeathScoreUI>().SetScore(this.score);

        RectTransform rect = scorePrefab.GetComponent<RectTransform>();
        RectTransform canvasRect = canvasTransform as RectTransform;

        Vector2 localPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Camera.main.WorldToScreenPoint(enemyPos),
            Camera.main,
            out localPos
        );

        rect.anchoredPosition = localPos;

        float displayTime = 0f;

        while (displayTime < duration)
        {
            displayTime += Time.deltaTime;
            rect.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(scorePrefab);
    }
}
