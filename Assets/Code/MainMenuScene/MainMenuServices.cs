using Code.Infrastructure.Views;
using Code.MainMenuScene.Models;
using Code.MainMenuScene.Views;
using Code.Services;
using Code.Storage;

namespace Code.MainMenuScene
{
    public class MainMenuServices
    {
        public GameConfigService GameConfigService { get; }
        public SceneService SceneService { get; }
        
        public ViewModelService ViewModelService { get; }

        public MainMenuServices(StorageSet storageSet)
        {
            GameConfigService = new GameConfigService(storageSet.GetStorage<GameConfigData>());
            SceneService = new SceneService(storageSet.GetStorage<SceneStorage>());

            ViewModelService = CreateViewModelService();
        }

        private ViewModelService CreateViewModelService()
        {
            var viewModelService = new ViewModelService();
            
            viewModelService.Register(new InstantiatedViewBuilder<MainMenuGameState, MainMenuGameStateView>());

            return viewModelService;
        }
    }
}