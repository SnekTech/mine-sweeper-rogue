using UnityEngine;

namespace SnekTech.UI.Effect
{
    /// <summary>
    /// convert an integer to a signed string representation
    /// </summary>
    public class SignedIntStr
    {
        private bool _isPositive;
        private readonly int _absValue;

        private string Sign => _isPositive ? "+" : "-";

        /// <summary>
        /// create a <see cref="SignedIntStr"/> object from an int, the sign will be determined by the value itself
        /// and stored in the object, the value will be stored as absolute value, the original value will be discarded
        /// </summary>
        /// <param name="value">int value to convert to a signed string representation,
        /// value zero is considered as positive by default, which can be changed through method <see cref="SetSign"/></param>
        public SignedIntStr(int value)
        {
            _absValue = Mathf.Abs(value);
            _isPositive = value >= 0;
        }

        /// <summary>
        /// set the sign of the object
        /// </summary>
        /// <param name="isPositive">sign</param>
        public void SetSign(bool isPositive)
        {
            _isPositive = isPositive;
        }

        public override string ToString()
        {
            return $"{Sign}{_absValue}";
        }
    }
}
