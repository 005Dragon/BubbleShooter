using System;
using System.Collections.Generic;
using Code.GameScene.Builders;
using Code.GameScene.Common;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class BubbleShooterAim : IModel, IUpdatable
    {
        public event EventHandler Updated; 
        public List<Vector2> Points { get; private set; }
        public bool Aiming => _userInput.Aiming;

        private readonly Vector2 _position;
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly UserInput _userInput;

        public BubbleShooterAim(Vector2 position, int maxIntersections, Map map, UserInput userInput)
        {
            _position = position;
            _userInput = userInput;

            _bubbleWayBuilder = new BubbleWayBuilder(map, maxIntersections);
        }

        public bool Update()
        {
            Points = new List<Vector2>() { _position };
            Vector2 direction = (_userInput.GetTargetPosition() - _position).normalized;
            Points.AddRange(_bubbleWayBuilder.Build(_position, direction));
            Updated?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }
}