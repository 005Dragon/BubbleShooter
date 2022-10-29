using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class BubbleService
    {
        private readonly Dictionary<BubbleType, StorableBubbleView> _bubbleTypeToStorableBubbleViewIndex;

        public BubbleService(BubbleViewStorage storage)
        {
            _bubbleTypeToStorableBubbleViewIndex = storage.SnakeSprites.ToDictionary(x => x.BubbleType, x => x);
        }

        public Sprite GetSprite(BubbleType bubbleType) => _bubbleTypeToStorableBubbleViewIndex[bubbleType].Sprite;
        public Color GetColor(BubbleType bubbleType) => _bubbleTypeToStorableBubbleViewIndex[bubbleType].Color;
    }
}