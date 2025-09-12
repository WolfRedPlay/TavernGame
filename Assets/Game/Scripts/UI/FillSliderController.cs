
using Core.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillSliderController : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private RectTransform _fillArea;
        [SerializeField] private RectTransform _goodAreaRect;
        [SerializeField] private RectTransform _perfectAreaRect;

        private ObservableValue<float> _currentTrackedValue;


        public void UpdatePerfectArea(FloatAreaOnRange area)
        {
            float minY = 0;
            float maxY = minY + _fillArea.rect.height;

            float downYPosition = Mathf.Lerp(minY, maxY, area.Min);
            float upYPosition = Mathf.Lerp(minY, maxY, area.Max);

            float areaHeight = upYPosition - downYPosition;

            _perfectAreaRect.anchoredPosition = new Vector2 (0, downYPosition);
            _perfectAreaRect.sizeDelta = new Vector2(_perfectAreaRect.sizeDelta.x, areaHeight);
        }

        public void UpdateGoodArea(FloatAreaOnRange area)
        {
            float minY = 0;
            float maxY = minY + _fillArea.rect.height;

            float downYPosition = Mathf.Lerp(minY, maxY, area.Min);
            float upYPosition = Mathf.Lerp(minY, maxY, area.Max);

            float areaHeight = upYPosition - downYPosition;

            _goodAreaRect.anchoredPosition = new Vector2 (0, downYPosition);
            _goodAreaRect.sizeDelta = new Vector2(_goodAreaRect.sizeDelta.x, areaHeight);
        }


        public void StartTrackSliderValue(ObservableValue<float> value)
        {
            _currentTrackedValue = value;

            _currentTrackedValue.OnValueChanged += UpdateSliderValue;
        }

        public void StopTrackSliderValue()
        {
            if (_currentTrackedValue != null)
            {
                UpdateSliderValue(0);
                _currentTrackedValue.OnValueChanged -= UpdateSliderValue;
            }
        }


        private void UpdateSliderValue(float newValue)
        {
            _slider.value = newValue;
        }
    }
}
