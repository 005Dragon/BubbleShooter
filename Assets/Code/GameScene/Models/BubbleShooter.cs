using System.Collections.Generic;
using Code.GameScene.Builders;
using Code.GameScene.Movers;
using Code.GameScene.Services;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class BubbleShooter
    {
        public Vector2 Position { get; set; }
        public float BubbleMoveSpeed { get; set; } = 1.0f;
        public float MaxAngle { get; set; } = 70.0f;
        
        private readonly BubbleBuilder _bubbleBuilder;
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly BubbleExploder _bubbleExploder;

        private readonly Map _map;
        private readonly BubbleMoverDispatcher _bubbleMoverDispatcher;

        private Bubble _readyBubble;
        private bool _ready;

        public BubbleShooter(
            Map map,
            UserInput userInput,
            BubbleMoverDispatcher bubbleMoverDispatcher,
            BubbleBuilder bubbleBuilder,
            BubbleExploder bubbleExploder)
        {
            _map = map;
            _bubbleMoverDispatcher = bubbleMoverDispatcher;

            _bubbleBuilder = bubbleBuilder;
            _bubbleWayBuilder = new BubbleWayBuilder(map, 100);
            _bubbleExploder = bubbleExploder;

            userInput.Shot += UserInputOnShot;
        }

        public void Charge()
        {
            _readyBubble = _bubbleBuilder.Build(BubbleService.GetRandomType(), Position);
            _ready = true;
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            if (!_ready)
            {
                return;
            }
            
            Vector2 direction = (targetPosition - Position).normalized;

            if (Mathf.Abs(Mathf.Atan2(direction.x, direction.y)) > MaxAngle * Mathf.Deg2Rad)
            {
                return;
            }

            List<Vector2> bubbleWay = _bubbleWayBuilder.Build(Position, direction);
            
            var bubbleMover = new BubbleMover(_readyBubble, BubbleMoveSpeed, bubbleWay.ToArray());
            _bubbleMoverDispatcher.Register(_readyBubble, bubbleMover);
            bubbleMover.MoveFinished += BubbleMoverOnMoveFinished;

            _map.Attach(_map.GetNearestFreeGridPosition(bubbleWay[^1]), _readyBubble, false);

            _ready = false;
        }

        private void BubbleMoverOnMoveFinished(object sender, Bubble bubble)
        {
            _bubbleExploder.TryExplosionGridChain(bubble);
            var bubbleMover = (BubbleMover)sender;
            bubbleMover.MoveFinished -= BubbleMoverOnMoveFinished;
            
            Charge();
        }
    }
}