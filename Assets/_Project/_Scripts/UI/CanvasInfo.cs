using System;
using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    /// <summary>
    /// read canvas information it lives with, singleton
    /// </summary>
    [RequireComponent(typeof(CanvasScaler))]
    public class CanvasInfo : MonoBehaviour
    {
        public static CanvasInfo Instance;

        private CanvasScaler _canvasScaler;

        public float ScaleFactor => transform.localScale.x; // assume the scale is constrained across axes
        public float ReferenceWidth => ReferenceResolution.x;
        public float ReferenceHeight => ReferenceResolution.y;

        private Vector2 ReferenceResolution => _canvasScaler.referenceResolution;
        
        private void Awake()
        {
            Instance = this;
            _canvasScaler = GetComponent<CanvasScaler>();
        }
    }
}