using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BubbleViewStorage), fileName = nameof(BubbleViewStorage))]
    public class BubbleViewStorage : ScriptableObject
    {
        [SerializeField] private List<StorableBubbleView> _snakeSprites;

        public IReadOnlyList<StorableBubbleView> SnakeSprites => _snakeSprites;
    }
}