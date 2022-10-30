using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class BubbleMoveBuilder
    {
        private readonly float _moveSpeed;
        private readonly UpdateBehavior _updateBehavior;

        public BubbleMoveBuilder(float moveSpeed, UpdateBehavior updateBehavior)
        {
            _moveSpeed = moveSpeed;
            _updateBehavior = updateBehavior;
        }

        public BubbleMover Build(Bubble bubble, List<Vector2> targets)
        {
            var bubbleMover = new BubbleMover(bubble, _moveSpeed, targets);
            
            _updateBehavior.AddToUpdate(bubbleMover);

            return bubbleMover;
        }
    }
}