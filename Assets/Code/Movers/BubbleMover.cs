using System;
using System.Collections.Generic;
using Code.Common;
using Code.Models;
using UnityEngine;

namespace Code.Movers
{
    public class BubbleMover : IUpdatable
    {
        public event EventHandler<Bubble> MoveFinished;
        
        private readonly Bubble _bubble;
        private readonly float _speed;
        private readonly Queue<Vector2> _targetPositions = new();

        private Vector2 _startPosition;
        private Vector2 _targetPosition;
        private float _progress;
        private float _distance;


        public BubbleMover(Bubble bubble, float speed, params Vector2[] targetPositions)
        {
            _bubble = bubble;
            _speed = speed;

            foreach (Vector2 targetPosition in targetPositions)
            {
                _targetPositions.Enqueue(targetPosition);
            }

            _targetPosition = _bubble.Position;
        }

        public bool Update()
        {
            bool needUpdate = (_bubble.Position - _targetPosition).magnitude > Calculating.Precision;

            if (!needUpdate)
            {
                _bubble.Position = _targetPosition;
                bool hasNextTarget = NextTarget();

                if (!hasNextTarget)
                {
                    MoveFinished?.Invoke(this, _bubble);
                    return false;
                }
            }
            
            _progress += _speed * Time.deltaTime / _distance;

            _bubble.Position = Vector2.Lerp(_startPosition, _targetPosition, _progress);

            return true;
        }

        private bool NextTarget()
        {
            if (_targetPositions.TryDequeue(out _targetPosition))
            {
                _startPosition = _bubble.Position;
                _distance = (_targetPosition - _startPosition).magnitude;
                _progress = 0;

                return true;
            }

            return false;
        }
    }
}