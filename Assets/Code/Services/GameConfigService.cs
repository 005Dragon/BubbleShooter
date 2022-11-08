using Code.Common;
using Code.Storage;
using UnityEngine;

namespace Code.Services
{
    public class GameConfigService
    {
        public LevelKey Level
        {
            get => _gameConfig.Level;
            set => _gameConfig.Level = value;
        }
        
        public float BubbleDiameter => _gameConfig.BubbleDiameter;
        public Vector2 BubbleShooterPosition => _gameConfig.BubbleShooterPosition;
        public int AimMaxIntersections => _gameConfig.AimMaxIntersections;
        public float BubbleSpeed => _gameConfig.BubbleSpeed;

        public float BubbleMinYPositionToGameOver => _gameConfig.BubbleMinYPositionToGameOver;
        
        private readonly GameConfigData _gameConfig;

        public GameConfigService(GameConfigData gameConfig)
        {
            _gameConfig = gameConfig;
        }
    }
}