using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarManager : MonoBehaviour
{
    public TextMeshProUGUI title_Text, min_Text, max_Text, unit_Text;
    public TMP_InputField input;
    public string title, unit;
    public float min, max;
    public Slider slider;
    public SettingList targetSetting;

    [Header ("Optional Data")]
    [Tooltip("SO 데이터가 필요한 특수 슬라이더만 넣어주세요. 없어도 작동합니다.")]
    [SerializeField] private GameSessionSO gameSessionSO;

    [Header("Rate SO Data")]
    [Tooltip("필수: SO 데이터가 Rate value (0~1)값인 경우 꼭 체크 해주세요")]
    [SerializeField] private bool isRateValue;

    private void OnEnable()
    {
        DifficultyController.OnDifficultyChanged += UpdateSliderValue;
        LobbyButtonManager.OnStartButtonClick += ApplyValueToSettingSO;
    }

    private void OnDisable()
    {
        DifficultyController.OnDifficultyChanged -= UpdateSliderValue;
        LobbyButtonManager.OnStartButtonClick -= ApplyValueToSettingSO;
    }

    
    void Awake()
    {
        //Setting minimum
        this.slider.minValue = this.min;
        min_Text.text = this.min.ToString();

        //Settimg maximum
        if (this.targetSetting.Equals(SettingList.WordCount))
        {
            if (gameSessionSO != null)
            {
                Debug.Log($"단어 출제 개수를 최대 {gameSessionSO.SystemPlayWordLimitCount}개로 제한합니다: SO 참조 가능");
                float fixedMax = Mathf.Min(this.max, gameSessionSO.SystemPlayWordLimitCount);
                this.slider.maxValue = fixedMax;
                max_Text.text = fixedMax.ToString();
            }

            else
            {
                Debug.LogWarning($"SO 데이터가 존재하지 않습니다. 최대 값을 {this.max}로 반환합니다.");
                this.slider.maxValue = this.max;
                max_Text.text = this.max.ToString();
            }
        }
        else
        {
            this.slider.maxValue = this.max;
            max_Text.text = this.max.ToString();
        }

        //Remind lobby setting SO value data
        if (this.targetSetting != SettingList.None)
        {
            if (this.isRateValue)
            {
                float rateValue = GameDataManager.Instance.RuntimeLobbySetting.GetValue(this.targetSetting);
                this.slider.value = Mathf.Lerp(this.min, this.max, rateValue);
                input.text = $"{this.slider.value:F1}";
            }
            else
            {
                this.slider.value = GameDataManager.Instance.RuntimeLobbySetting.GetValue(this.targetSetting);
                input.text = $"{this.slider.value:F1}";
            }
        }
        else
        {
            Debug.LogWarning($"슬라이더 바 내 {SettingList.None} 감지");
            this.slider.value = this.min;
            input.text = $"{this.slider.value}";
        }

        //Setting UI text
        title_Text.text = this.title;
        
        if (this.unit != null)
        {
            unit_Text.text = this.unit;
        }

        else
        {
            unit_Text.text = "";
        }
    }

    public void UpdateSliderFromInput()
    {
        if (string.IsNullOrEmpty(this.input.text))
        {
            this.input.text = this.slider.value.ToString("F1");
            return;
        }

        if (float.TryParse(this.input.text, out float value))
        {
            float clampValue = Mathf.Clamp(value, this.slider.minValue, this.slider.maxValue);
            this.slider.value = clampValue;
            this.input.text = clampValue.ToString("F1");
        }

        else
        {
            this.input.text = this.slider.value.ToString("F1");
        }
    }

    public void UpdateInputFromSlider()
    {
        this.input.text = this.slider.value.ToString("F1");
    }

    //Difficulty Button
    public void UpdateSliderValue() //GetValue(SettingList target)
    {
        float value = GameDataManager.Instance.RuntimeLobbySetting.GetValue(this.targetSetting);

        //If value is rate; rate -> whole number
        if (this.targetSetting == SettingList.EnemySpeedRate)
        {
            float rawValue = (this.slider.maxValue - this.slider.minValue)* value + this.slider.minValue;
            this.slider.value = Mathf.RoundToInt(rawValue); //Whole Numbers is true
        }
        else
        {
            this.slider.value = value;
        }
        
        this.input.text = this.slider.value.ToString("F1");
    }

    public void ApplyValueToSettingSO()
    {
        //Whole number -> rate
        if (this.targetSetting == SettingList.EnemySpeedRate)
        {
            float rate = (this.slider.value - this.slider.minValue) / (this.slider.maxValue - this.slider.minValue);
            GameDataManager.Instance.RuntimeLobbySetting.SetValue(this.targetSetting, rate);
        }
        else
        {
            GameDataManager.Instance.RuntimeLobbySetting.SetValue(this.targetSetting, this.slider.value);
        }
        
    }
}
