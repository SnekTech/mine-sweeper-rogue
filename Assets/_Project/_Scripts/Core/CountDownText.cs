using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace SnekTech.Core
{
    public class CountDownText : MonoBehaviour, ICountDownDisplay
    {
        [SerializeField]
        private TMP_Text text;

        public void UpdateDurationRemaining(float durationRemaining)
        {
            text.text = durationRemaining.ToString("F1", CultureInfo.InvariantCulture);
        }

        public void SetActive(bool isActive)
        {
            text.gameObject.SetActive(isActive);
        }
    }
}
