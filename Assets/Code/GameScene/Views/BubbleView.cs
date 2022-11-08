using Code.Common;
using Code.GameScene.Models;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.GameScene.Views
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BubbleView : ViewBase<Bubble>
    {
        public Sprite Sprite { get; set; }
        public Color Color { get; set; }

        private SpriteRenderer _spriteRenderer;

        public override void Initialize(IModel model)
        {
            base.Initialize(model);
            
            _spriteRenderer.sprite = Sprite;
            _spriteRenderer.color = Color;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}