using Code.Common;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(StorableBubbleView), fileName = nameof(StorableBubbleView))]
    public class StorableBubbleView : ScriptableObject
    {
        [SerializeField] private BubbleType _bubbleType;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;

        public BubbleType BubbleType => _bubbleType;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
}