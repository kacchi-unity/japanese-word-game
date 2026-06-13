using TMPro;
using UnityEngine;

public class EPCountManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI epCountText;

    void Start()
    {
        int EP = GameDataManager.Instance.GetData<GameSessionSO>().EnlightenmentPoint;
        epCountText.text = $"{EP}";
    }

}
