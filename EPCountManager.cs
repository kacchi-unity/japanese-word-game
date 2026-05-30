using TMPro;
using UnityEngine;

public class EPCountManager : MonoBehaviour
{
    [SerializeField] private GameSessionSO gameSessionSO;
    [SerializeField] private TextMeshProUGUI epCountText;

    void Start()
    {
        if (gameSessionSO != null)
        {
            epCountText.text = $"{gameSessionSO.EnlightenmentPoint}";
        }

        else
        {
            epCountText.text = "Not Fount";
        }
    }

}
