using Code.Services;

namespace Code.Common.Models.Builders
{
    public class MainMenuNavigationPointBuilder
    {
        private readonly SceneService _sceneService;
        private readonly ViewModelDispatcher _viewModelDispatcher;

        public MainMenuNavigationPointBuilder(SceneService sceneService, ViewModelDispatcher viewModelDispatcher)
        {
            _sceneService = sceneService;
            _viewModelDispatcher = viewModelDispatcher;
        }

        public MainMenuNavigationPoint Build()
        {
            var mainMenuNavigationPoint = new MainMenuNavigationPoint(_sceneService.GetSceneName(SceneKey.MainMenu));
            
            _viewModelDispatcher.ConstructViewModel(mainMenuNavigationPoint);

            return mainMenuNavigationPoint;
        }
    }
}