using SnekTech.GridCell;
using UnityEngine;

namespace Tests.PlayMode
{
    public class CoverBehaviourBuilder : BehaviourBuilder<CoverBehaviour>
    {
        private bool _isActive = true;
        
        public CoverBehaviourBuilder(string prefabName) : base(prefabName)
        {
        }

        public CoverBehaviourBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        protected override CoverBehaviour Build()
        {
            CoverBehaviour cover = Object.Instantiate(BehaviourPrefab);
            cover.gameObject.SetActive(_isActive);

            return cover;
        }
    }
}
