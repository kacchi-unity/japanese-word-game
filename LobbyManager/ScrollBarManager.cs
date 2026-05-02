using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarManager : MonoBehaviour
{

    public TextMeshProUGUI title_Text, min_Text, max_Text, currentValue_Text;
    public TMP_InputField input;
    public string title;
    public float min, max, currentValue;
    public Slider slider;


    void Start()
    {
        title_Text.text = this.title;
        min_Text.text = this.min.ToString();
        max_Text.text = this.max.ToString();

        this.slider.minValue = this.min;
        this.slider.maxValue = this.max;
        this.slider.wholeNumbers = true;

        this.slider.value = this.min;
        input.text = this.min.ToString();

    }

    public void UpdateSliderFromInput()
    {
        if (string.IsNullOrEmpty(this.input.text))
        {
            input.text = this.slider.value.ToString();
            return;
        }

        if (float.TryParse(this.input.text, out float value))
        {
            float clampValue = Mathf.Clamp(value, this.slider.minValue, this.slider.maxValue);
            this.slider.value = clampValue;
            input.text = clampValue.ToString();
        }

        else
        {
            input.text = this.slider.value.ToString();
        }
    }

    public void UpdateInputFromSlider()
    {
        input.text = this.slider.value.ToString();
    }
}
