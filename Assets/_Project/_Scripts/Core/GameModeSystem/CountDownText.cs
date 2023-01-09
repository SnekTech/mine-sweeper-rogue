using System.Globalization;
using TMPro;
using UnityEngine;

namespace SnekTech.Core.GameModeSystem
{
    public class CountDownText : MonoBehaviour, ICountDownDisplay
    {
        [SerializeField]
        private TMP_Text text;

        public bool IsActive
        {
            get => text.GetActiveSelf();
            set => text.SetActive(value);
        }

        public void UpdateDurationRemaining(float durationRemaining)
        {
            text.text = durationRemaining.ToString("F1", CultureInfo.InvariantCulture);
        }
    }
}