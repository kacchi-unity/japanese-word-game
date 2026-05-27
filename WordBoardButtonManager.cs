using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WordBoardButtonManager : MonoBehaviour
{
    [Header ("UI Connection")]
    [SerializeField] private Button battleStartButton;
    [SerializeField] private CanvasGroup wordBoardCanvasGroup;

    [Header("Value Setting")]
    [SerializeField] private float fadeOutDuration = 1.0f;

    public static event Action OnBattleStartButtonClick;

    private void OnEnable()
    {
        battleStartButton.onClick.AddListener(BattleStart);
    }

    private void OnDisable()
    {
        battleStartButton.onClick.RemoveListener(BattleStart);
    }

    void BattleStart()
    {
        StartCoroutine(WordBoardCanvasFadeOut());
        OnBattleStartButtonClick?.Invoke();
    }

    IEnumerator WordBoardCanvasFadeOut()
    {
        wordBoardCanvasGroup.alpha = 1f;
        float elapsed = 0f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            wordBoardCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / fadeOutDuration);
            yield return null;
        }

        wordBoardCanvasGroup.alpha = 0f;
        wordBoardCanvasGroup.gameObject.SetActive(false);
    }
}
