using System;
using Code.Common;
using Code.Services;

namespace Code.GameScene.Models.Builders
{
    public class PauseBuilder
    {
        private readonly GameUserInput _userInput;
        private readonly Action<bool> _stopUpdate;
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly SceneService _sceneService;

        public PauseBuilder(
            GameUserInput userInput, 
            Action<bool> stopUpdate, 
            ViewModelDispatcher viewModelDispatcher, 
            SceneService sceneService)
        {
            _userInput = userInput;
            _stopUpdate = stopUpdate;
            _viewModelDispatcher = viewModelDispatcher;
            _sceneService = sceneService;
        }

        public Pause Build()
        {
            var pauseStartGameNavigationPoint =
                new PauseStartGameNavigationPoint(_sceneService.GetSceneName(SceneKey.Game));
            
            var pauseMainMenuNavigationPoint =
                new PauseMainMenuNavigationPoint(_sceneService.GetSceneName(SceneKey.MainMenu));

            var pause = new Pause(_userInput, _stopUpdate);
            
            _viewModelDispatcher.ConstructViewModel(pauseStartGameNavigationPoint);
            _viewModelDispatcher.ConstructViewModel(pauseMainMenuNavigationPoint);
            _viewModelDispatcher.ConstructViewModel(pause);

            return pause;
        }
    }
}