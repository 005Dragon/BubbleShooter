using UnityEngine;

namespace Code
{
    public class BubbleMover : IUpdatable
    {
        private const float Precision = 0.001f;

        private readonly Vector2 _startPosition;
        private readonly Vector2 _targetPosition;
        private readonly float _distance;
        private readonly Bubble _bubble;
        private readonly float _speed;

        private float _progress;

        public BubbleMover(Bubble bubble, Vector2 targetPosition, float speed)
        {
            _bubble = bubble;
            _startPosition = _bubble.Position;
            _targetPosition = targetPosition;
            _distance = (_targetPosition - _startPosition).magnitude;
            _speed = speed;
        }

        public bool Update()
        {
            _progress += _speed * Time.deltaTime / _distance;

            _bubble.Position = Vector2.Lerp(_startPosition, _targetPosition, _progress);
            
            bool needUpdate = (_bubble.Position - _targetPosition).magnitude > Precision;

            if (!needUpdate)
            {
                _bubble.Position = _targetPosition;
            }

            return needUpdate;
        }
    }
}