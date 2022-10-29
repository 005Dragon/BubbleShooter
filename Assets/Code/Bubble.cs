using System;
using UnityEngine;

namespace Code
{
    public class Bubble : IModel
    {
        public event EventHandler<Vector2> PositionChanged;
        
        public BubbleType BubbleType { get; }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                PositionChanged?.Invoke(this, _position);
            }
        }

        private Vector2 _position;

        public Bubble(BubbleType bubbleType, Vector2 position)
        {
            BubbleType = bubbleType;
            Position = position;
        }
    }
}