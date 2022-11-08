using Code.GameScene.Models;
using Code.GameScene.Models.Builders;
using Code.GameScene.Views;
using Code.GameScene.Views.Builders;
using Code.Infrastructure.Views;
using Code.Services;
using Code.Storage;
using UnityEngine;

namespace Code.GameScene
{
    public class GameServices
    {
        public CameraService CameraService { get; }
        public GameUserInputService UserInputService { get; }
        public GameConfigService GameConfigService { get; }
        public BubbleService BubbleService { get; }
        public LevelService LevelService { get; }
        public SceneService SceneService { get; }
        
        public IUpdateService UpdateService { get; }
        
        public ViewModelService ViewModelService { get; }
        
        public BubbleMoverService BubbleMoverService { get; }

        public GameServices(Camera camera, StorageSet storageSet, IUpdateService updateService)
        {
            CameraService = new CameraService(camera);
            UserInputService = new GameUserInputService(camera);
            
            UpdateService = updateService;

            GameConfigService = new GameConfigService(storageSet.GetStorage<GameConfigData>());
            BubbleService = new BubbleService(storageSet.GetStorage<BubbleViewStorage>());
            LevelService = new LevelService(storageSet.GetStorage<LevelStorage>());
            SceneService = new SceneService(storageSet.GetStorage<SceneStorage>());

            ViewModelService = CreateViewModelService();
            BubbleMoverService = CreateBubbleMoverService();
        }
        
        private ViewModelService CreateViewModelService()
        {
            var viewModelDispatcher = new ViewModelService();

            viewModelDispatcher.Register(new BubbleViewBuilder(BubbleService));
            viewModelDispatcher.Register(new InstantiatedViewBuilder<BubbleShooterAim, BubbleShooterAimView>());
            viewModelDispatcher.Register(new InstantiatedViewBuilder<GameOverGameState, GameOverGameStateView>());
            viewModelDispatcher.Register(new InstantiatedViewBuilder<GameVictoryGameState, GameVictoryGameStateView>());
            viewModelDispatcher.Register(new InstantiatedViewBuilder<PauseGameState, PauseGameStateView>());

            viewModelDispatcher.Register(new BubbleBuilder(GameConfigService.BubbleDiameter));

            return viewModelDispatcher;
        }
        
        private BubbleMoverService CreateBubbleMoverService()
        {
            var bubbleMoverDispatcher = new BubbleMoverService();

            UpdateService.AddToUpdate(bubbleMoverDispatcher);

            return bubbleMoverDispatcher;
        }
    }
}