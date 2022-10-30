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

            userInput.Shot += UserInputOnShot;
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            Bubble bubble = _bubbleBuilder.Build(BubbleService.GetRandomType(), _position);

            Vector2 direction = (targetPosition - _position).normalized;

            List<Vector2> bubbleWay =_bubbleWayBuilder.Build(_position, direction, 10);

            _bubbleMoveBuilder.Build(bubble, bubbleWay);

            _map.AttachToGrid(_map.GetNearestFreeGridPosition(bubbleWay[^1]), bubble, false);
        }
    }
}