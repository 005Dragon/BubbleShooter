using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class Map
    {
        public event EventHandler FullnessChanged;
        
        public IReadOnlyCollection<BorderLine> BorderLines => _borderLines;

        public IEnumerable<Vector2Int> GridPositions => 
            Enumerable.Range(0, _grid.Length).Select(x => new Vector2Int(x % GridSize.x, x / GridSize.x));

        public IEnumerable<Vector2> EmployedPositions =>
            _gridPositionToWorldPositionIndex
                .Where(x => this[x.Key] != null)
                .Select(x => x.Value);

        public Bubble this[Vector2Int gridPosition]
        {
            get => _grid[gridPosition.x + gridPosition.y * GridSize.x];
            private set => _grid[gridPosition.x + gridPosition.y * GridSize.x] = value;
        }
        
        public Vector2Int GridSize { get; private set; }
        public Vector2 ElementSize { get; private set; }

        private readonly BorderLine[] _borderLines = new BorderLine[3];
        private readonly Rect _worldPlayZone;
        private readonly Dictionary<Vector2Int, Vector2> _gridPositionToWorldPositionIndex = new();

        private Bubble[] _grid;
        private Vector2 _elementShift;
        private float _horizontalMargin;

        public Map(GameServices gameServices)
        {
            Camera camera = gameServices.CameraService.Camera;
            
            Vector2 minPlayZonePoint = camera.ScreenToWorldPoint(Vector3.zero);
            Vector2 maxPlayZonePoint =
                camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));

            _worldPlayZone = new Rect(minPlayZonePoint, maxPlayZonePoint - minPlayZonePoint);
        }

        public void Construct(Vector2 elementSize)
        {
            ElementSize = elementSize;
            _elementShift = elementSize / 2.0f;
            
            _borderLines[0] = new BorderLine(Orientation.Horizontal, _worldPlayZone.yMax - _elementShift.y);
            _borderLines[1] = new BorderLine(Orientation.Vertical, _worldPlayZone.xMin + _elementShift.x);
            _borderLines[2] = new BorderLine(Orientation.Vertical, _worldPlayZone.xMax - _elementShift.y);

            GridSize = new Vector2Int(
                (int)((_worldPlayZone.width - _elementShift.x) / ElementSize.x),
                (int)(_worldPlayZone.height / ElementSize.y)
            );

            _horizontalMargin = (_worldPlayZone.width - GridSize.x * ElementSize.x - _elementShift.x) / 2.0f;
            
            _grid = new Bubble[GridSize.x * GridSize.y];

            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    var gridPosition = new Vector2Int(x, y);

                    _gridPositionToWorldPositionIndex[gridPosition] = CalculateWorldPositionByGridPosition(gridPosition);
                }
            }
        }

        public void Attach(Vector2Int gridPosition, Bubble bubble, bool setPositionOnGrid = true)
        {
            this[gridPosition] = bubble;

            if (setPositionOnGrid)
            {
                bubble.Position = _gridPositionToWorldPositionIndex[gridPosition];
            }
            
            FullnessChanged?.Invoke(this, EventArgs.Empty);
        }

        public Bubble Detach(Vector2Int gridPosition)
        {
            Bubble detachedBubble = this[gridPosition];

            if (detachedBubble == null)
            {
                return null;
            }
            
            this[gridPosition] = null;

            FullnessChanged?.Invoke(this, EventArgs.Empty);
            
            return detachedBubble;
        }
        
        public Vector2Int GetNearestFreeGridPosition(Vector2 position)
        {
            IEnumerable<KeyValuePair<Vector2Int, Vector2>> freeGridPositions = 
                _gridPositionToWorldPositionIndex.Where(x => this[x.Key] == null);

            float minDistance = float.MaxValue;
            Vector2Int minDistanceGridPosition = default;
            
            foreach (KeyValuePair<Vector2Int,Vector2> gridPositionToWorldPositionPair in freeGridPositions)
            {
                float distance = (position - gridPositionToWorldPositionPair.Value).magnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    minDistanceGridPosition = gridPositionToWorldPositionPair.Key;
                }
            }

            return minDistanceGridPosition;
        }

        public Vector2 GetWorldPositionByGridPosition(Vector2Int girdPosition)
        {
            return _gridPositionToWorldPositionIndex[girdPosition];
        }

        public IEnumerable<Vector2Int> GetAreaGridPositions(Vector2Int gridPosition)
        {
            if (gridPosition.x > 0)
            {
                yield return new Vector2Int(gridPosition.x - 1, gridPosition.y);
            }

            if (gridPosition.x < GridSize.x - 1)
            {
                yield return new Vector2Int(gridPosition.x + 1, gridPosition.y);
            }

            if (gridPosition.y > 0)
            {
                yield return new Vector2Int(gridPosition.x, gridPosition.y - 1);

                if (gridPosition.y % 2 == 0)
                {
                    if (gridPosition.x < GridSize.x - 1)
                    {
                        yield return new Vector2Int(gridPosition.x + 1, gridPosition.y - 1);
                    }
                }
                else
                {
                    if (gridPosition.x > 0)
                    {
                        yield return new Vector2Int(gridPosition.x - 1, gridPosition.y - 1);
                    }
                }
            }

            if (gridPosition.y < GridSize.y - 1)
            {
                yield return new Vector2Int(gridPosition.x, gridPosition.y + 1);
                
                if (gridPosition.y % 2 == 0)
                {
                    if (gridPosition.x < GridSize.x - 1)
                    {
                        yield return new Vector2Int(gridPosition.x + 1, gridPosition.y + 1);
                    }
                }
                else
                {
                    if (gridPosition.x > 0)
                    {
                        yield return new Vector2Int(gridPosition.x - 1, gridPosition.y + 1);
                    }
                }
            }
        }

        public bool TryGetGridPosition(Bubble bubble, out Vector2Int gridPosition)
        {
            for (int i = 0; i < _grid.Length; i++)
            {
                if (_grid[i] == bubble)
                {
                    gridPosition = new Vector2Int(i % GridSize.x, i / GridSize.x);
                    return true;
                }
            }

            gridPosition = default;
            return false;
        }

        private Vector2 CalculateWorldPositionByGridPosition(Vector2Int girdPosition)
        {
            Vector2 result = new Vector2(
                _worldPlayZone.xMin + _horizontalMargin + _elementShift.x + girdPosition.x * ElementSize.x,
                _worldPlayZone.yMax - (_elementShift.y + girdPosition.y * ElementSize.y)
            );

            bool needShift = girdPosition.y % 2 == 0;
            
            if (needShift)
            {
                result = new Vector2(result.x + _elementShift.x, result.y);
            }

            return result;
        }
    }
}