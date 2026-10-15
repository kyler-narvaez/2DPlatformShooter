using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    [SerializeField] private TextIndicators textIndicator;

    // Sets max health of bar
    public void SetMaxValue(int newValue)
    {
        slider.maxValue = newValue;
        fill.color = gradient.Evaluate(1f);
    }
    // Sets health of bar
    public void SetValue(int newValue)
    {
        slider.value = newValue;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetTextIndicator(int newValue)
    {
        textIndicator.SetTextValue("", newValue);
    }

    public void SetMinValue(int value)
    {
        slider.minValue = value;
    }

    public int GetBarValue()
    {
        return (int)GetComponent<Slider>().value;
    }
}
