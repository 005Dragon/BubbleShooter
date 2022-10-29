using UnityEngine;

namespace Code
{
    public class BubbleShooter
    {
        private readonly Vector2 _position;
        private readonly BubbleBuilder _bubbleBuilder;
        private readonly BubbleMoveBuilder _bubbleMoveBuilder;

        public BubbleShooter(
            Vector2 position, 
            BubbleBuilder bubbleBuilder, 
            BubbleMoveBuilder bubbleMoveBuilder,
            UserInput userInput)
        {
            _position = position;
            _bubbleBuilder = bubbleBuilder;
            _bubbleMoveBuilder = bubbleMoveBuilder;

            userInput.Shot += UserInputOnShot;
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            Bubble bubble = _bubbleBuilder.Build(BubbleType.Red, _position);

            Vector2 direction = (targetPosition - _position).normalized;

            _bubbleMoveBuilder.Build(bubble, direction);
        }
    }
}