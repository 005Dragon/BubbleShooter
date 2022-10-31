using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class BubbleWayBuilder
    {
        private readonly int _maxIntersections;
        
        private readonly Map _map;

        public BubbleWayBuilder(Map map, int maxIntersections)
        {
            _map = map;
            _maxIntersections = maxIntersections;
        }

        public List<Vector2> Build(Vector2 startPosition, Vector2 direction)
        {
            List<Vector2> intersectionPoints = GetIntersectionPoints(startPosition, direction, _maxIntersections);

            intersectionPoints[^1] =
                _map.GetWorldPositionByGridPosition(_map.GetNearestFreeGridPosition(intersectionPoints[^1]));

            return intersectionPoints;
        }

        private List<Vector2> GetIntersectionPoints(Vector2 startPoint, Vector2 direction, int maxIntersections)
        {
            var result = new List<Vector2>();

            Vector2 intersectionPoint;
            
            while (true)
            {
                bool intersectedWithOtherBubbles =
                    TryGetIntersectionWithOtherBubbles(startPoint, direction, out intersectionPoint);

                if (intersectedWithOtherBubbles)
                {
                    result.Add(intersectionPoint);
                    return result;
                }

                GetIntersectionWithBorders(
                    startPoint,
                    direction,
                    out intersectionPoint,
                    out var intersectionBorderLine
                );
                
                result.Add(intersectionPoint);

                if (result.Count >= maxIntersections)
                {
                    return result;
                }

                startPoint = intersectionPoint;
                direction = intersectionBorderLine.Orientation == Orientation.Vertical
                    ? new Vector2(-direction.x, direction.y)
                    : new Vector2(direction.x, -direction.y);

                if (intersectionBorderLine.Orientation == Orientation.Horizontal)
                {
                    break;
                }
            }

            if (TryGetIntersectionWithOtherBubbles(startPoint, direction, out intersectionPoint))
            {
                result.Add(intersectionPoint);
            }

            return result;
        }

        private bool TryGetIntersectionWithOtherBubbles(
            Vector2 startPoint, 
            Vector2 direction,
            out Vector2 intersectionPoint)
        {
            foreach (Vector2 position in _map.EmployedPositions.OrderBy(x => (x - startPoint).magnitude))
            {
                bool intersected = Calculating.TryGetIntersection(
                    startPoint,
                    direction,
                    position,
                    _map.ElementSize.x / 2,
                    out intersectionPoint
                );

                if (intersected)
                {
                    return true;
                }
            }

            intersectionPoint = default;
            return false;
        }

        private void GetIntersectionWithBorders(
            Vector2 startPoint, 
            Vector2 direction,
            out Vector2 intersectionPoint,
            out BorderLine intersectionBorderLine)
        {
            var minIntersectionDistance = float.MaxValue;
            Vector2 minIntersectionDistancePoint = default;
            BorderLine minIntersectionDistanceBorderLint = default;

            foreach (BorderLine borderLine in _map.BorderLines)
            {
                if (Calculating.TryGetIntersection(startPoint, direction, borderLine, out intersectionPoint))
                {
                    float intersectionDistance = (startPoint - intersectionPoint).magnitude;

                    if (minIntersectionDistance > intersectionDistance)
                    {
                        minIntersectionDistance = intersectionDistance;
                        minIntersectionDistancePoint = intersectionPoint;
                        minIntersectionDistanceBorderLint = borderLine;
                    }
                }
            }

            intersectionPoint = minIntersectionDistancePoint;
            intersectionBorderLine = minIntersectionDistanceBorderLint;
        }
    }
}