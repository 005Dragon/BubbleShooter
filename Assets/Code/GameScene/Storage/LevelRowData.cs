using System.Collections.Generic;
using Code.GameScene.Common;
using UnityEngine;

namespace Code.GameScene.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(LevelRowData), fileName = nameof(LevelRowData))]
    public class LevelRowData : ScriptableObject
    {
        [SerializeField] private List<BubbleType> _bubbleTypes;

        public IReadOnlyList<BubbleType> BubbleTypes => _bubbleTypes;
    }
}