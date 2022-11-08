using Code.Common;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(GameConfigData), fileName = nameof(GameConfigData))]
    public class GameConfigData : ScriptableObject
    {
        public LevelKey Level;
        public Vector2 BubbleShooterPosition;
        public float BubbleSpeed;
        public float BubbleDiameter;
        public float BubbleMinYPositionToGameOver;
        public int AimMaxIntersections;
    }
}