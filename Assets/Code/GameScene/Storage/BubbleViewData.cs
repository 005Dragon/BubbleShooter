using Code.GameScene.Common;
using UnityEngine;

namespace Code.GameScene.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BubbleViewData), fileName = nameof(BubbleViewData))]
    public class BubbleViewData : ScriptableObject
    {
        [SerializeField] private BubbleType _bubbleType;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;

        public BubbleType BubbleType => _bubbleType;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
}