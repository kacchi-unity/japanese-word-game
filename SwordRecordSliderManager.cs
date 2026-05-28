using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwordRecordSliderBarManager : MonoBehaviour
{
    public static event Action<bool> isSwordRecordSliderActive;

    public TextMeshProUGUI title_Text, min_Text, max_Text, unit_Text, input_Text;
    public TMP_InputField input;
    public string title, unit;
    public float min, max;
    public Slider slider;

    public GameObject wordSlider, swordRecordSlider;
    public RectTransform wordSliderPos, swordRecordSliderPos;
    public Button cancel;

    [Header("SO Data")]
    [SerializeField] private GameSessionSO gameSessionSO;

    void OnEnable()
    {
        cancel.onClick.AddListener(Deactivate);
    }

    void OnDisable()
    {
        cancel.onClick.RemoveListener(Deactivate);
    }

    void Start()
    {
        if (SceneTracker.previousScene.Equals(SceneTracker.SceneType.SwordRecord))
        {
            isSwordRecordSliderActive?.Invoke(true);

            Debug.Log(SceneTracker.selectorSwordRecordWordList.Count);
            Debug.Log(SceneTracker.previousScene);
            title_Text.text = this.title;
            min_Text.text = this.min.ToString();
            this.slider.minValue = this.min;

            if (gameSessionSO != null)
            {
                float fixedMax = Mathf.Min(this.max, gameSessionSO.SystemPlayWordLimitCount);
                this.slider.maxValue = fixedMax;
                max_Text.text = fixedMax.ToString();
            }
            
            
            unit_Text.text = this.unit;

            wordSlider.SetActive(false);
            swordRecordSliderPos = wordSliderPos;

            swordRecordSlider.SetActive(true);

            if (SceneTracker.selectorSwordRecordWordList != null)
            {
                slider.value = SceneTracker.selectorSwordRecordWordList.Count;
            }
            else if(SceneTracker.selectorSwordRecordWordList == null)
            {
                Debug.LogWarning("SceneTracker 내 selectorSwordRecordWordList 가 null입니다.");
                return;
            }
            else if (SceneTracker.selectorSwordRecordWordList.Count == 0)
            {
                Debug.LogWarning("SceneTracker 내 selectorSwordRecordWordList 인덱스 개수가 0입니다.");
                return;
            }

            slider.interactable = false;

            input_Text.text = slider.value.ToString();

            input.interactable = false;
        }

        else
        {
            Deactivate();
        }
    }

    void Deactivate()
    {
        swordRecordSlider.SetActive(false);
        wordSlider.SetActive(true);
        isSwordRecordSliderActive?.Invoke(false);

    }
}
