using Code.Common;
using Code.Storage;

namespace Code.MainMenuScene
{
    public class GameLevelChanger : IModel
    {
        public LevelKey LevelKey
        {
            get => _gameConfig.Level;
            set => _gameConfig.Level = value;
        }
        
        private readonly GameConfigData _gameConfig;

        public GameLevelChanger(GameConfigData gameConfig)
        {
            _gameConfig = gameConfig;
        }
    }
}