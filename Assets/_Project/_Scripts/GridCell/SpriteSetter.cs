using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSetter : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        
        public Sprite Sprite
        {
            set => _renderer.sprite = value;
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
    }
}
