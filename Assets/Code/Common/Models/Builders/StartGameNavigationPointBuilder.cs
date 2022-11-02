using Code.Services;

namespace Code.Common.Models.Builders
{
    public class StartGameNavigationPointBuilder
    {
        private readonly SceneService _sceneService;
        private readonly ViewModelDispatcher _viewModelDispatcher;

        public StartGameNavigationPointBuilder(SceneService sceneService, ViewModelDispatcher viewModelDispatcher)
        {
            _sceneService = sceneService;
            _viewModelDispatcher = viewModelDispatcher;
        }

        public StartGameNavigationPoint Build()
        {
            var startGameNavigationPoint = new StartGameNavigationPoint(_sceneService.GetSceneName(SceneKey.Game));
            
            _viewModelDispatcher.ConstructViewModel(startGameNavigationPoint);

            return startGameNavigationPoint;
        }
    }
}