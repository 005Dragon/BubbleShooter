using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class BubbleExploder
    {
        public int MinChainLength { get; set; } = 3;
        
        private readonly Map _map;
        private readonly BubbleBuilder _bubbleBuilder;

        public BubbleExploder(Map map, BubbleBuilder bubbleBuilder)
        {
            _map = map;
            _bubbleBuilder = bubbleBuilder;
        }

        public bool TryExplosionGridChain(Bubble bubble)
        {
            BubbleType bubbleType = bubble.BubbleType;

            List<Vector2Int> chainGridPositions = new();

            Vector2Int gridPosition = _map.GetGridPosition(bubble);
            
            FillChainGridPositions(chainGridPositions, bubbleType, gridPosition);

            if (chainGridPositions.Count >= MinChainLength)
            {
                foreach (Vector2Int chainGridPosition in chainGridPositions)
                {
                    Bubble detachedBubble = _map.Detach(chainGridPosition);
                    detachedBubble.Destroy();
                }

                return true;
            }

            return false;
        }

        private void FillChainGridPositions(
            ICollection<Vector2Int> chainGridPositions, 
            BubbleType bubbleType, 
            Vector2Int gridPosition,
            List<Vector2Int> excludedGridPositions = null)
        {
            excludedGridPositions ??= new List<Vector2Int>();

            foreach (Vector2Int areaGridPosition in _map.GetAreaGridPositions(gridPosition))
            {
                if (excludedGridPositions.Contains(areaGridPosition))
                {
                    continue;
                }

                excludedGridPositions.Add(areaGridPosition);

                Bubble bubble = _map[areaGridPosition];

                if (bubble != null && bubble.BubbleType == bubbleType)
                {
                    chainGridPositions.Add(areaGridPosition);
                    FillChainGridPositions(chainGridPositions, bubbleType, areaGridPosition, excludedGridPositions);
                }
            }
        }
    }
}