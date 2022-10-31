using System;
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
            _cachedTransform.localScale = new Vector2(_model.Diameter, _model.Diameter);
            
            _spriteRenderer.sprite = Sprite;
            _spriteRenderer.color = Color;
            
            _model.PositionChanged += ModelOnPositionChanged;
            _model.Destroyed += ModelOnDestroyed;
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

        private void ModelOnDestroyed(object sender, EventArgs eventArgs)
        {
            _model.PositionChanged -= ModelOnPositionChanged;
            _model.Destroyed -= ModelOnDestroyed;
            
            gameObject.SetActive(false);
        }
    }
}