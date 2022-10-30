using UnityEngine;

namespace Code
{
    public static class Calculating
    {
        public const float Precision = 0.0001f;

        public static bool TryGetIntersection(
            Vector2 startPoint, 
            Vector2 direction, 
            Vector2 centerCircle,
            float radius,
            out Vector2 intersectionPoint)
        {
            Vector2 toCenterCircleDirection = (centerCircle - startPoint);
            
            float distance = toCenterCircleDirection.magnitude;

            float cosAngle = (direction.x * toCenterCircleDirection.x + direction.y * toCenterCircleDirection.y) /
                             (direction.magnitude * distance);
            
            float distanceStartPointToNearestPoint = distance * cosAngle;

            Vector2 nearestPoint = startPoint + direction.normalized * distanceStartPointToNearestPoint;

            float distanceNearestPointToCenterCircle = (nearestPoint - centerCircle).magnitude;

            if (distanceNearestPointToCenterCircle > radius)
            {
                intersectionPoint = default;
                return false;
            }
            
            if (radius - distanceNearestPointToCenterCircle < Precision)
            {
                intersectionPoint = nearestPoint;
                return true;
            }

            float shiftDistance = 
                Mathf.Sqrt(radius * radius - distanceNearestPointToCenterCircle * distanceNearestPointToCenterCircle);

            intersectionPoint = startPoint + direction * (distanceStartPointToNearestPoint - shiftDistance);
            return true;
        }
        
        public static bool TryGetIntersection(
            Vector2 startPoint, 
            Vector2 direction, 
            BorderLine borderLine,
            out Vector2 intersectionPoint)
        {
            if (borderLine.Orientation == Orientation.Horizontal)
            {
                float directionToBorder = borderLine.Position - startPoint.y;

                bool intersectionImpossible =
                    direction.y == 0 ||
                    direction.y > 0 && directionToBorder < 0 ||
                    direction.y < 0 && directionToBorder > 0 ||
                    Mathf.Abs(directionToBorder) < Calculating.Precision;

                if (intersectionImpossible)
                {
                    intersectionPoint = default;
                    return false;
                }

                float angleCoefficient = direction.x / direction.y;

                intersectionPoint =
                    new Vector2(startPoint.x + angleCoefficient * directionToBorder, borderLine.Position);
                return true;
            }
            else
            {
                float directionToBorder = borderLine.Position - startPoint.x;
                
                bool intersectionImpossible =
                    direction.x == 0 ||
                    direction.x > 0 && directionToBorder < 0 ||
                    direction.x < 0 && directionToBorder > 0 || 
                    Mathf.Abs(directionToBorder) < Calculating.Precision;

                if (intersectionImpossible)
                {
                    intersectionPoint = default;
                    return false;
                }
                
                float angleCoefficient = direction.y / direction.x;

                intersectionPoint =
                    new Vector2(borderLine.Position, startPoint.y + angleCoefficient * directionToBorder);
                return true;
            }
        }
    }
}