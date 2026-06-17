using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CorrectDetailsSectionManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnHoverEnter;
    public event Action OnHoverExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit?.Invoke();
    }
}
