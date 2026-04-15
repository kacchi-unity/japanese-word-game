using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class Popup : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Button confirmButton, closeButton;
    public CanvasGroup canvasGroup;

    private Action onClickConfirm;

    public void Setup(string message, Action onConfirm)
    {
        StartCoroutine(FadeIn());
        messageText.text = message;
        onClickConfirm = onConfirm;

        //confirm Button
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            onClickConfirm?.Invoke();
            StartCoroutine(FadeOut());
        });

        //close Button
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() =>
        {
            onClickConfirm?.Invoke();
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
