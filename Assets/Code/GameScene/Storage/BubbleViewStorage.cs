using System.Collections.Generic;
using UnityEngine;

namespace Code.GameScene.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BubbleViewStorage), fileName = nameof(BubbleViewStorage))]
    public class BubbleViewStorage : ScriptableObject
    {
        [SerializeField] private List<BubbleViewData> _bubbleViews;

        public IReadOnlyList<BubbleViewData> BubbleViews => _bubbleViews;
    }
}