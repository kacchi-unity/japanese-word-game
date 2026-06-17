
using UnityEngine;

public class CorrectDetailsShowManager : MonoBehaviour
{
    [Header("마우스 오버 영역 UI")]
    [Tooltip("마우스 오버 감지 오브젝트")]
    [SerializeField] private CorrectDetailsSectionManager infoSection;

    [Header("활성화/비활성화 대상 오브젝트")]
    [Tooltip("마우스 오버 시 활성화할 오브젝트")]
    [SerializeField] private GameObject infoCloud;

    private void OnEnable()
    {
        infoSection.OnHoverEnter += ShowDetail;
        infoSection.OnHoverExit += HideDetail;
    }

    private void OnDisable()
    {
        infoSection.OnHoverEnter -= ShowDetail;
        infoSection.OnHoverExit -= HideDetail;
    }

    private void Awake()
    {
        if (infoCloud != null)
        {
            infoCloud.SetActive(false);
        }
    }

    private void ShowDetail() => SetActiveDetail(true);
    private void HideDetail() => SetActiveDetail(false);

    private void SetActiveDetail(bool isActive)
    {
        if (infoCloud != null)
        {
            transform.SetAsLastSibling();
            infoCloud.SetActive(isActive);
        }

        else
        {
            Debug.LogWarning($"{infoCloud.name} 참조 불가. 로직을 중단합니다.");
        }
    }
}
