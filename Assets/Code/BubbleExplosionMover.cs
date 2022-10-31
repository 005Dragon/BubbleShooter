using System;
using UnityEngine;

namespace Code
{
    public class BubbleExplosionMover : IUpdatable
    {
        public event EventHandler<Bubble> MoveFinished;

        public float GravityForce { get; set; } = -9.8f;
        public float Duration { get; set; } = 3.0f;
        
        private readonly Bubble _bubble;
        
        private Vector2 _speed;

        public BubbleExplosionMover(Bubble bubble, Vector2 explosionCenter, float explosionForce)
        {
            _bubble = bubble;

            Vector2 directionToBubblePosition = (bubble.Position - explosionCenter);
            float distanceToExplosion = directionToBubblePosition.magnitude;

            if (distanceToExplosion > 0)
            {
                _speed = directionToBubblePosition.normalized * (explosionForce / distanceToExplosion);
            }
        }

        public bool Update()
        {
            if (Duration <= 0)
            {
                MoveFinished?.Invoke(this, _bubble);
                return false;
            }
            
            _bubble.Position += _speed * Time.deltaTime;
            _speed = new Vector2(_speed.x, _speed.y + GravityForce * Time.deltaTime);
            Duration -= Time.deltaTime;

            return true;
        }
    }
}