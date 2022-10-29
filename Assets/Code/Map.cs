using UnityEngine;

namespace Code
{
    public class Map
    {
        private readonly Camera _camera;
        private readonly BorderLine[] _borderLines = new BorderLine[3];

        public Map(Camera camera)
        {
            _camera = camera;
        }

        public void Construct(float mapWidth)
        {
            _borderLines[0] = new BorderLine(Orientation.Horizontal, GetMaxHeight(_camera));
            _borderLines[1] = new BorderLine(Orientation.Vertical, -mapWidth / 2.0f);
            _borderLines[2] = new BorderLine(Orientation.Vertical, mapWidth / 2.0f);
        }

        public Vector2 GetIntersectionPoint(Vector2 startPoint, Vector2 direction, out Orientation borderOrientation)
        {
            float minIntersectionDistance = float.MaxValue;
            Vector2 result = default;
            borderOrientation = default;

            foreach (var borderLine in _borderLines)
            {
                if (TryGetIntersection(startPoint, direction, borderLine, out Vector2 intersectionPoint))
                {
                    float intersectionDistance = (intersectionPoint - startPoint).magnitude; 
                    
                    if (intersectionDistance < minIntersectionDistance)
                    {
                        minIntersectionDistance = intersectionDistance;
                        borderOrientation = borderLine.Orientation;
                        result = intersectionPoint;
                    }
                }
            }

            return result;
        }

        private float GetMaxHeight(Camera camera)
        {
            return camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight)).y;
        }

        private bool TryGetIntersection(
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