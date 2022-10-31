using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class BubbleShooter
    {
        private readonly Vector2 _position;
        private readonly BubbleBuilder _bubbleBuilder;
        private readonly BubbleMoveBuilder _bubbleMoveBuilder;
        private readonly BubbleWayBuilder _bubbleWayBuilder;
        private readonly Map _map;
        private readonly BubbleExploder _bubbleExploder;

        private Bubble _readyBubble;

        public BubbleShooter(
            Vector2 position, 
            BubbleBuilder bubbleBuilder, 
            BubbleMoveBuilder bubbleMoveBuilder,
            BubbleWayBuilder bubbleWayBuilder,
            Map map,
            UserInput userInput)
        {
            _position = position;
            _bubbleBuilder = bubbleBuilder;
            _bubbleMoveBuilder = bubbleMoveBuilder;
            _bubbleWayBuilder = bubbleWayBuilder;
            _map = map;

            _bubbleExploder = new BubbleExploder(map, bubbleBuilder);

            userInput.Shot += UserInputOnShot;
        }

        public void Charge()
        {
            _readyBubble = _bubbleBuilder.Build(BubbleService.GetRandomType(), _position);
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - _position).normalized;

            List<Vector2> bubbleWay =_bubbleWayBuilder.Build(_position, direction, 10);

            BubbleMover bubbleMover = _bubbleMoveBuilder.Build(_readyBubble, bubbleWay);
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