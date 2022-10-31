using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
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
        
        private readonly BubbleBuilder _bubbleBuilder;
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly BubbleExploder _bubbleExploder;

        private readonly Map _map;
        private readonly Action<IUpdatable> _addToUpdate;
        
        private Bubble _readyBubble;

        public BubbleShooter(
            Map map,
            UserInput userInput,
            Action<IUpdatable> addToUpdate,
            ViewModelDispatcher viewModelDispatcher)
        {
            _map = map;
            _addToUpdate = addToUpdate;

            _bubbleBuilder = new BubbleBuilder(viewModelDispatcher);
            _bubbleWayBuilder = new BubbleWayBuilder(map, BubbleMaxIntersections);
            _bubbleExploder = new BubbleExploder(map, _bubbleBuilder);

            userInput.Shot += UserInputOnShot;
        }

        public void Charge()
        {
            _readyBubble = _bubbleBuilder.Build(BubbleService.GetRandomType(), Position);
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - Position).normalized;

            List<Vector2> bubbleWay =_bubbleWayBuilder.Build(Position, direction);
            
            var bubbleMover = new BubbleMover(_readyBubble, BubbleMoveSpeed, bubbleWay);
            _addToUpdate.Invoke(bubbleMover);
            bubbleMover.MoveFinished += BubbleMoverOnMoveFinished;

            _map.Attach(_map.GetNearestFreeGridPosition(bubbleWay[^1]), _readyBubble, false);
            
            Charge();
        }

        private void BubbleMoverOnMoveFinished(object sender, Bubble bubble)
        {
            _bubbleExploder.TryExplosionGridChain(bubble);
        }
    }
}