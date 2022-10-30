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

        public float Diameter { get; }

        private Vector2 _position;

        public Bubble(BubbleType bubbleType, Vector2 position, float diameter)
        {
            BubbleType = bubbleType;
            Position = position;
            Diameter = diameter;
        }
    }
}