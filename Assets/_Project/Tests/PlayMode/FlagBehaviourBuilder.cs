using SnekTech.GridCell;
using UnityEngine;

namespace Tests.PlayMode
{
    public class FlagBehaviourBuilder : BehaviourBuilder<FlagBehaviour>
    {
        public FlagBehaviourBuilder(string prefabName) : base(prefabName)
        {
        }
 
        private bool _isActive = true;

        public FlagBehaviourBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        protected override FlagBehaviour Build()
        {
            FlagBehaviour flag = Object.Instantiate(BehaviourPrefab);
            flag.gameObject.SetActive(_isActive);

            return flag;
        }

        public static implicit operator FlagBehaviour(FlagBehaviourBuilder builder)
        {
            return builder.Build();
        }

       
    }
}
