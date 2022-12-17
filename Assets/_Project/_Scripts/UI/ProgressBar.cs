using System;
using SnekTech.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image fillImage;

        private float _min;
        private float _current;
        private float _max = 1;

        private TooltipTrigger _tooltipTrigger;

        public float Min
        {
            get => _min;
            set
            {
                _min = value;
                CalculateFillAmount();
            }
        }

        public float Max
        {
            get => _max;
            set
            {
                _max = value;
                CalculateFillAmount();
            }
        }

        private string Content => $"{_current}/{_max}";

        public float CurrentValue
        {
            get => _current;
            set
            {
                if (!value.WithinRange(_min, _max))
                {
                    throw new ArgumentOutOfRangeException($"value [{value}] out of range [{_min}, {_max}]");
                }

                _current = value;
                CalculateFillAmount();
            }
        }

        private void Awake()
        {
            _tooltipTrigger = GetComponent<TooltipTrigger>();
        }

        public void Init(float min, float max, float current)
        {
            if (min > max)
            {
                throw new ArgumentException($"min[{min}] > max[{max}]");
            }
            _min = min;
            _max = max;
            CurrentValue = current;
        }

        private void CalculateFillAmount()
        {
            fillImage.fillAmount = (_current - _min) / (_max - _min);
            _tooltipTrigger.SetContent(new TooltipContent("", Content));
        }
    }
}