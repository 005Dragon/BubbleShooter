using System;
using Code.Common;
using Code.Services;

namespace Code.GameScene.Models.Builders
{
    public class GameVictoryBuilder
    {
        private readonly Map _map;
        private readonly Level _level;
        private readonly Action<bool> _stopUpdate;
        
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly SceneService _sceneService;

        public GameVictoryBuilder(
            Map map, 
            Level level, 
            Action<bool> stopUpdate, 
            ViewModelDispatcher viewModelDispatcher,
            SceneService sceneService)
        {
            _map = map;
            _level = level;
            _stopUpdate = stopUpdate;
            _viewModelDispatcher = viewModelDispatcher;
            _sceneService = sceneService;
        }

        public GameVictory Build()
        {
            var gameVictoryMainMenuNavigationPoint =
                new GameVictoryMainMenuNavigationPoint(_sceneService.GetSceneName(SceneKey.MainMenu));
            
            var gameVictory = new GameVictory(_map, _level, _stopUpdate);

            _viewModelDispatcher.ConstructViewModel(gameVictoryMainMenuNavigationPoint);
            _viewModelDispatcher.ConstructViewModel(gameVictory);

            return gameVictory;
        }
    }
}