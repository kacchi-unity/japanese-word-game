using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class ConfirmModal : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Button confirmButton, cancleButton;
    public CanvasGroup canvasGroup;

    private Action onClickConfirm;
    private Action onClickCancle;

    public void Setup(string message, Action onConfirm, Action onCancle)
    {
        StartCoroutine(FadeIn());
        messageText.text = message;
        
        //confirm Button
        onClickConfirm = onConfirm;
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            onClickConfirm?.Invoke();
            StartCoroutine(FadeOut());
        });

        //confirm Button
        onClickCancle = onCancle;
        cancleButton.onClick.RemoveAllListeners();
        cancleButton.onClick.AddListener(() =>
        {
            onClickCancle?.Invoke();
            StartCoroutine(FadeOut());
        });

    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0f;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * 5;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 5;
            yield return null;
        }
        Destroy(gameObject);
    }
}
