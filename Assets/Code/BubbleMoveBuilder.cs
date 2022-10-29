using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class BubbleMoveBuilder
    {
        public int MaxIntersections { get; set; } = 4;
        
        private readonly float _moveSpeed;
        private readonly Map _map;
        private readonly UpdateBehavior _updateBehavior;

        public BubbleMoveBuilder(float moveSpeed, Map map, UpdateBehavior updateBehavior)
        {
            _moveSpeed = moveSpeed;
            _map = map;
            _updateBehavior = updateBehavior;
        }

        public BubbleMover Build(Bubble bubble, Vector2 direction)
        {
            var bubbleMover = new BubbleMover(bubble, _moveSpeed, GetTargetPoints(bubble.Position, direction));
            
            _updateBehavior.AddToUpdate(bubbleMover);

            return bubbleMover;
        }

        private IEnumerable<Vector2> GetTargetPoints(Vector2 startPoint, Vector2 direction)
        {
            for (int currentTargetPointIndex = 0; currentTargetPointIndex < MaxIntersections; currentTargetPointIndex++)
            {
                Vector2 intersectionPoint = _map.GetIntersectionPoint(startPoint, direction, out var orientation);

                yield return intersectionPoint;

                direction = orientation == Orientation.Horizontal
                    ? new Vector2(direction.x, -direction.y)
                    : new Vector2(-direction.x, direction.y);

                startPoint = intersectionPoint;
            }
        }
    }
}