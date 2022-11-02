using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameScene.Common;
using Code.GameScene.Movers;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class BubbleExploder
    {
        public event EventHandler<bool> ExplosionFinished;
        
        public int MinChainLength { get; set; } = 3;
        public float ExplosionForce { get; set; } = 2.0f;
        
        private readonly Map _map;
        private readonly BubbleMoverDispatcher _bubbleMoverDispatcher;

        public BubbleExploder(Map map, BubbleMoverDispatcher bubbleMoverDispatcher)
        {
            _map = map;
            _bubbleMoverDispatcher = bubbleMoverDispatcher;
        }

        public bool TryExplosionGridChain(Bubble bubble)
        {
            BubbleType bubbleType = bubble.BubbleType;

            if (!_map.TryGetGridPosition(bubble, out Vector2Int gridPosition))
            {
                ExplosionFinished?.Invoke(this, false);
                return false;
            }
            
            HashSet<Vector2Int> chainGridPositions = new();
            
            FillChainGridPositions(chainGridPositions, bubbleType, gridPosition);

            if (chainGridPositions.Count >= MinChainLength)
            {
                foreach (Vector2Int chainGridPosition in chainGridPositions)
                {
                    Bubble detachedBubble = _map.Detach(chainGridPosition);
                    Explosion(detachedBubble, bubble.Position);
                }

                IEnumerable<Vector2Int> gridPositionsWithoutSupport = 
                    _map.GridPositions.Where(x => _map[x] != null).Except(GetGridPositionsWithSupport());

                foreach (Vector2Int gridPositionWithoutSupport in gridPositionsWithoutSupport)
                {
                    Bubble detachedBubble = _map.Detach(gridPositionWithoutSupport);
                    Explosion(detachedBubble, bubble.Position);
                }

                ExplosionFinished?.Invoke(this, true);
                return true;
            }

            ExplosionFinished?.Invoke(this, false);
            return false;
        }

        private HashSet<Vector2Int> GetGridPositionsWithSupport()
        {
            var result = new HashSet<Vector2Int>();

            IEnumerable<Vector2Int> mainSupportGridPositions = Enumerable
                .Range(0, _map.GridSize.x)
                .Select(x => new Vector2Int(x, 0))
                .Where(x => _map[x] != null);

            foreach (Vector2Int gridPosition in mainSupportGridPositions)
            {
                FillArea(gridPosition);
            }

            return result;

            void FillArea(Vector2Int gridPosition)
            {
                if (result.Add(gridPosition))
                {
                    IEnumerable<Vector2Int> area = _map.GetAreaGridPositions(gridPosition).Where(x => _map[x] != null);
                    
                    foreach (Vector2Int areaGridPosition in area)
                    {
                        FillArea(areaGridPosition);
                    }
                }
            }
        }

        private void FillChainGridPositions(
            HashSet<Vector2Int> chainGridPositions, 
            BubbleType bubbleType, 
            Vector2Int gridPosition,
            List<Vector2Int> excludedGridPositions = null)
        {
            excludedGridPositions ??= new List<Vector2Int>();

            if (excludedGridPositions.Contains(gridPosition))
            {
                return;
            }
            
            excludedGridPositions.Add(gridPosition);

            Bubble bubble = _map[gridPosition];

            if (bubble != null && bubble.BubbleType == bubbleType)
            {
                chainGridPositions.Add(gridPosition);

                foreach (Vector2Int areaGridPosition in _map.GetAreaGridPositions(gridPosition))
                {
                    FillChainGridPositions(chainGridPositions, bubbleType, areaGridPosition, excludedGridPositions);
                }
            }
        }

        private void Explosion(Bubble bubble, Vector2 explosionCenter)
        {
            var bubbleExplosionMover = new BubbleExplosionMover(bubble, explosionCenter, ExplosionForce);
            _bubbleMoverDispatcher.Register(bubble, bubbleExplosionMover);
            bubbleExplosionMover.MoveFinished += BubbleExplosionMoverOnMoveFinished;
        }

        private void BubbleExplosionMoverOnMoveFinished(object sender, Bubble bubble)
        {
            bubble.Destroy();
            var bubbleExplosionMover = (BubbleExplosionMover)sender;
            bubbleExplosionMover.MoveFinished -= BubbleExplosionMoverOnMoveFinished;
        }
    }
}