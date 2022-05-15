using System;
using TMPro;
using UnityEngine;

namespace SnekTech.UI
{
    public class DamageEffectController : MonoBehaviour
    {
        private TMP_Text _tmpText;

        private void Awake()
        {
            _tmpText = GetComponentInChildren<TMP_Text>();
        }

        public void SetText<T>(T text)
        {
            _tmpText.SetText(text.ToString());
        }
    }
}
