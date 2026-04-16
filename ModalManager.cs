using UnityEngine;
using System;

public class ModalManager : MonoBehaviour
{
    public static ModalManager Instance;

    public AlertModal alertModalPrefab;
    public ConfirmModal confirmModalPrefab;
    public Transform canvasTransform;

    public bool isAnyModalActive = false;

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

    public void ShowAlertModal(string message, Action onOK = null)
    {
        //Avoid duplication prefab
        if (isAnyModalActive == true)
        {
            return;
        }

        isAnyModalActive = true;
        AlertModal currentModal = Instantiate(alertModalPrefab, canvasTransform);
        
        currentModal.Setup(message, () =>
        {
            onOK?.Invoke();
            isAnyModalActive = false;
        } );
    }

    public void ShowConfirmModal(string message, Action onConfirm = null, Action onCancle = null)
    {
        //Avoid duplication prefab
        if (isAnyModalActive == true)
        {
            return;
        }

        isAnyModalActive = true;
        ConfirmModal currentModal = Instantiate(confirmModalPrefab, canvasTransform);

        currentModal.Setup(message,
            () => {
                onConfirm?.Invoke();
                isAnyModalActive = false;
            } ,

            () => {
                onCancle?.Invoke();
                isAnyModalActive = false;
            } );
    }


}
