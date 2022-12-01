using System;

namespace SnekTech.InventorySystem.Items
{
    public class ReachLimitException<T> : Exception
    {
        public T ActualChangeAmount { get; }

        private const string Msg =
            "original value would exceed limit after adding the desired change amount, so the value has been clamped";
        
        public ReachLimitException(){}
        
        public ReachLimitException(string message) : base(message){}
        
        public ReachLimitException(string message, Exception inner) : base(message, inner){}

        public ReachLimitException(T actualChangeAmount)
            : this(Msg)
        {
            ActualChangeAmount = actualChangeAmount;
        }
    }
}
