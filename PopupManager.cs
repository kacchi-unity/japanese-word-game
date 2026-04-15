using UnityEngine;
using System;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public Popup popupPrefab;
    public Transform canvasTransform;

    private Popup currentPopup;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPopup(string message, Action onConfirm = null)
    {
        //Avoid duplication prefab
        if (currentPopup != null)
        {
            return;
        }

        currentPopup = Instantiate(popupPrefab, canvasTransform);

        currentPopup.Setup(message, () =>
        {
            onConfirm?.Invoke();
            currentPopup = null;
        } );
    }

    
}
