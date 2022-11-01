using System.Collections.Generic;
using Code.Builders;
using Code.Movers;
using Code.Services;
using Code.Views;
using UnityEngine;

namespace Code.Models
{
    public class BubbleShooter
    {
        public Vector2 Position { get; set; }
        public float BubbleMoveSpeed { get; set; } = 1.0f;
        public float BubbleDiameter
        {
            get => _bubbleBuilder.Diameter;
            set => _bubbleBuilder.Diameter = value;
        }
        public int BubbleMaxIntersections { get; set; } = 20;
        public float MaxAngle { get; set; } = 70.0f;
        
        private readonly BubbleBuilder _bubbleBuilder;
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly BubbleExploder _bubbleExploder;

        private readonly Map _map;
        private readonly BubbleMoverDispatcher _bubbleMoverDispatcher;

        private Bubble _readyBubble;

        public BubbleShooter(
            Map map,
            UserInput userInput,
            BubbleMoverDispatcher bubbleMoverDispatcher,
            ViewModelDispatcher viewModelDispatcher)
        {
            _map = map;
            _bubbleMoverDispatcher = bubbleMoverDispatcher;

            _bubbleBuilder = new BubbleBuilder(viewModelDispatcher);
            _bubbleWayBuilder = new BubbleWayBuilder(map, BubbleMaxIntersections);
            _bubbleExploder = new BubbleExploder(map, bubbleMoverDispatcher);

            userInput.Shot += UserInputOnShot;
        }

        public void Charge()
        {
            _readyBubble = _bubbleBuilder.Build(BubbleService.GetRandomType(), Position);
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - Position).normalized;

            if (Mathf.Abs(Mathf.Atan2(direction.x, direction.y)) > MaxAngle * Mathf.Deg2Rad)
            {
                return;
            }

            List<Vector2> bubbleWay =_bubbleWayBuilder.Build(Position, direction);
            
            var bubbleMover = new BubbleMover(_readyBubble, BubbleMoveSpeed, bubbleWay);
            _bubbleMoverDispatcher.Register(_readyBubble, bubbleMover);
            bubbleMover.MoveFinished += BubbleMoverOnMoveFinished;

            _map.Attach(_map.GetNearestFreeGridPosition(bubbleWay[^1]), _readyBubble, false);
            
            Charge();
        }

        private void BubbleMoverOnMoveFinished(object sender, Bubble bubble)
        {
            _bubbleExploder.TryExplosionGridChain(bubble);
            var bubbleMover = (BubbleMover)sender;
            bubbleMover.MoveFinished -= BubbleMoverOnMoveFinished;
        }
    }
}