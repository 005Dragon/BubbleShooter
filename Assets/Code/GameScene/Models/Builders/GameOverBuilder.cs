using System;
using Code.Common;
using Code.Common.Models;
using Code.Services;

namespace Code.GameScene.Models.Builders
{
    public class GameOverBuilder
    {
        private readonly Map _map;
        private readonly BubbleExploder _bubbleExploder;
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly Action<bool> _stopUpdate;
        private readonly SceneService _sceneService;

        public GameOverBuilder(
            Map map, 
            BubbleExploder bubbleExploder, 
            ViewModelDispatcher viewModelDispatcher,
            Action<bool> stopUpdate,
            SceneService sceneService)
        {
            _map = map;
            _bubbleExploder = bubbleExploder;
            _viewModelDispatcher = viewModelDispatcher;
            _stopUpdate = stopUpdate;
            _sceneService = sceneService;
        }

        public GameOver Build()
        {
            var startGameNavigationPoint = new StartGameNavigationPoint(_sceneService.GetSceneName(SceneKey.Game));
            var gameOverMinHeightLine = new GameOver.MinHeightLine();
            var gameOver = new GameOver(gameOverMinHeightLine, _map, _bubbleExploder, _stopUpdate);
            
            var mainMenuNavigationPoint = 
                new GameOverMainMenuNavigationPoint(_sceneService.GetSceneName(SceneKey.MainMenu));
            
            _viewModelDispatcher.ConstructViewModel(startGameNavigationPoint);
            _viewModelDispatcher.ConstructViewModel(mainMenuNavigationPoint);
            _viewModelDispatcher.ConstructViewModel(gameOverMinHeightLine);
            _viewModelDispatcher.ConstructViewModel(gameOver);

            return gameOver;
        }
    }
}