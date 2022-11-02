using System;
using System.Collections.Generic;
using Code.Common;
using Code.GameScene.Models.Builders;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class BubbleShooterAim : IModel, IUpdatable
    {
        public event EventHandler Updated; 
        public List<Vector2> Points { get; private set; }
        public bool Aiming => _gameUserInput.Aiming;

        private readonly Vector2 _position;
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly GameUserInput _gameUserInput;

        public BubbleShooterAim(Vector2 position, int maxIntersections, Map map, GameUserInput gameUserInput)
        {
            _position = position;
            _gameUserInput = gameUserInput;

            _bubbleWayBuilder = new BubbleWayBuilder(map, maxIntersections);
        }

        public bool Update()
        {
            Points = new List<Vector2>() { _position };
            Vector2 direction = (_gameUserInput.GetTargetPosition() - _position).normalized;
            Points.AddRange(_bubbleWayBuilder.Build(_position, direction));
            Updated?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }
}