using System.Collections.Generic;
using Code.GameScene.Models.Builders;
using Code.GameScene.Movers;
using Code.Services;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class BubbleShooter
    {
        public Vector2 Position { get; set; }
        public float BubbleMoveSpeed { get; set; } = 1.0f;
        public float MaxAngle { get; set; } = 70.0f;
        
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly BubbleExploder _bubbleExploder;

        private readonly Map _map;
        private readonly ViewModelService _viewModelService;
        private readonly BubbleMoverService _bubbleMoverService;

        private Bubble _readyBubble;
        private bool _ready;

        public BubbleShooter(Map map, Level level, GameServices gameServices)
        {
            _map = map;
            _bubbleExploder = level.BubbleExploder;
            
            _viewModelService = gameServices.ViewModelService;
            _bubbleMoverService = gameServices.BubbleMoverService;

            _bubbleWayBuilder = new BubbleWayBuilder(_map, 100);

            gameServices.UserInputService.Shot += UserInputOnShot;
        }

        public void Charge()
        {
            _readyBubble = _viewModelService.ConstructViewModel<Bubble, Bubble.Settings>(
                new Bubble.Settings
                {
                    BubbleType = BubbleService.GetRandomType(),
                    Position = Position
                }
            );
            
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
            _bubbleMoverService.Register(_readyBubble, bubbleMover);
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