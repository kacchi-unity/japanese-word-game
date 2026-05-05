using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarManager : MonoBehaviour
{

    public TextMeshProUGUI title_Text, min_Text, max_Text, unit_Text;
    public TMP_InputField input;
    public string title, unit;
    public float min, max, currentValue;
    public Slider slider;
    public SettingList targetSetting;

    public LobbySettingSO lobbySetting;

    private void OnEnable()
    {
        DifficultyController.OnDifficultyChanged += UpdateSliderValue;
        GameStartManager.OnStartButtonClick += ApplyValueToSettingSO;
    }

    private void OnDisable()
    {
        DifficultyController.OnDifficultyChanged -= UpdateSliderValue;
        GameStartManager.OnStartButtonClick -= ApplyValueToSettingSO;
    }


    void Start()
    {
        title_Text.text = this.title;
        unit_Text.text = this.unit;
        min_Text.text = this.min.ToString();
        max_Text.text = this.max.ToString();

        this.slider.minValue = this.min;
        this.slider.maxValue = this.max;

        this.slider.value = this.min;
        input.text = this.min.ToString();

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
        this.slider.value = lobbySetting.settingValue.GetValue(this.targetSetting);

        this.input.text = this.slider.value.ToString("F1");
    }

    public void ApplyValueToSettingSO()
    {
        lobbySetting.settingValue.SetValue(this.targetSetting, this.slider.value);
    }
}
