using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BubbleView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }
        public Sprite Sprite { get; set; }
        public Color Color { get; set; }

        private Bubble _model;
        
        private SpriteRenderer _spriteRenderer;
        private Transform _cachedTransform;
        
        public void Initialize()
        {
            _model = (Bubble)Model;

            _cachedTransform.position = _model.Position;
            
            _spriteRenderer.sprite = Sprite;
            _spriteRenderer.color = Color;
            
            _model.PositionChanged += ModelOnPositionChanged;
        }

        private void Awake()
        {
            _cachedTransform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void ModelOnPositionChanged(object sender, Vector2 position)
        {
            _cachedTransform.position = position;
        }
    }
}