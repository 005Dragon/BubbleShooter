using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public static BubbleType GetRandomType()
        {
            Array bubbleTypeValues = Enum.GetValues(typeof(BubbleType));

            int bubbleTypeIndex = Random.Range(0, bubbleTypeValues.Length);
            int currentBubbleTypeIndex = 0;

            foreach (object bubbleTypeValue in bubbleTypeValues)
            {
                if (bubbleTypeIndex == currentBubbleTypeIndex)
                {
                    return (BubbleType)bubbleTypeValue;
                }

                currentBubbleTypeIndex++;
            }

            return default;
        }
    }
}