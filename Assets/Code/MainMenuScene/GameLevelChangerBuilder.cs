using Code.Common;
using Code.Storage;

namespace Code.MainMenuScene
{
    public class GameLevelChangerBuilder
    {
        private readonly GameConfigData _gameConfig;
        private readonly ViewModelDispatcher _viewModelDispatcher;

        public GameLevelChangerBuilder(GameConfigData gameConfig, ViewModelDispatcher viewModelDispatcher)
        {
            _gameConfig = gameConfig;
            _viewModelDispatcher = viewModelDispatcher;
        }

        public GameLevelChanger Build()
        {
            var gameLevelChanger = new GameLevelChanger(_gameConfig);
            
            _viewModelDispatcher.ConstructViewModel(gameLevelChanger);

            return gameLevelChanger;
        }
    }
}