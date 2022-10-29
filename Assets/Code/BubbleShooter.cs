using System;
using UnityEngine;

namespace Code
{
    public class BubbleShooter
    {
        public float BubbleSpeed { get; set; } = 1.0f;
        
        private readonly Vector2 _position;
        private readonly BubbleBuilder _bubbleBuilder;
        private readonly Action<IUpdatable> _registerUpdatableObject;

        public BubbleShooter(
            Vector2 position, 
            BubbleBuilder bubbleBuilder, 
            UserInput userInput,
            Action<IUpdatable> registerUpdatableObject)
        {
            _position = position;
            _bubbleBuilder = bubbleBuilder;
            _registerUpdatableObject = registerUpdatableObject;

            userInput.Shot += UserInputOnShot;
        }

        private void UserInputOnShot(object sender, Vector2 targetPosition)
        {
            Bubble bubble = _bubbleBuilder.Build(BubbleType.Red, _position);

            BubbleMover bubbleMover = new BubbleMover(bubble, targetPosition, BubbleSpeed);

            _registerUpdatableObject(bubbleMover);
        }
    }
}