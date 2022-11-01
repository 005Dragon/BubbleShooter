using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.Storage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Services
{
    public class BubbleService
    {
        private readonly Dictionary<BubbleType, BubbleViewData> _bubbleTypeToStorableBubbleViewIndex;

        public BubbleService(BubbleViewStorage storage)
        {
            _bubbleTypeToStorableBubbleViewIndex = storage.BubbleViews.ToDictionary(x => x.BubbleType, x => x);
        }

        public Sprite GetSprite(BubbleType bubbleType) => _bubbleTypeToStorableBubbleViewIndex[bubbleType].Sprite;
        public Color GetColor(BubbleType bubbleType) => _bubbleTypeToStorableBubbleViewIndex[bubbleType].Color;
        public static BubbleType GetRandomType(bool excludeNone = true)
        {
            Array bubbleTypeValues = Enum.GetValues(typeof(BubbleType));

            int startIndex = excludeNone ? 1 : 0;
            int bubbleTypeIndex = Random.Range(startIndex, bubbleTypeValues.Length);
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