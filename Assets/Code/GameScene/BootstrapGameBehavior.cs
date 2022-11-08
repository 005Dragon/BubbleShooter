using Code.Common;
using Code.GameScene.Models;
using Code.Services;
using Code.Storage;
using UnityEngine;

namespace Code.GameScene
{
    [RequireComponent(typeof(UpdateBehavior))]
    public class BootstrapGameBehavior : MonoBehaviour
    {
        [SerializeField] private StorageSet _storageSet;

        private GameUserInputService _userInputService;

        private void Awake()
        {
            var updateBehavior = GetComponent<UpdateBehavior>();

            var gameServices = new GameServices(Camera.main, _storageSet, updateBehavior);
            _userInputService = gameServices.UserInputService;
            
            Map map = CreateMap(gameServices);
            Level level = CreateLevel(map, gameServices);
            
            CreateAim(map, gameServices);
            CreateBubbleShooter(map, level, gameServices);
            
            CreateGameStates(map, level, gameServices);
        }

        private void OnEnable() => _userInputService.Enable();

        private void OnDisable() => _userInputService.Disable();

        private Map CreateMap(GameServices gameServices)
        {
            var map = new Map(gameServices);

            float bubbleDiameter = gameServices.GameConfigService.BubbleDiameter;

            map.Construct(new Vector2(bubbleDiameter, bubbleDiameter));

            return map;
        }

        private Level CreateLevel(Map map, GameServices gameServices)
        {
            var level = new Level(map, gameServices);
            
            level.Construct(gameServices.GameConfigService.Level);

            return level;
        }
        
        private void CreateAim(Map map, GameServices gameServices)
        {
            var bubbleShooterAim = new BubbleShooterAim(
                new BubbleShooterAim.Settings
                {
                    Position = gameServices.GameConfigService.BubbleShooterPosition,
                    Map = map,
                    MaxIntersections = gameServices.GameConfigService.AimMaxIntersections,
                    UserInputService = gameServices.UserInputService
                }
            );
            
            gameServices.ViewModelService.ConstructViewModel(bubbleShooterAim);
            gameServices.UpdateService.AddToUpdate(bubbleShooterAim);
        }

        private void CreateBubbleShooter(Map map, Level level, GameServices gameServices)
        {
            var bubbleShooter = new BubbleShooter(map, level, gameServices)
            {
                Position = gameServices.GameConfigService.BubbleShooterPosition,
                BubbleMoveSpeed = gameServices.GameConfigService.BubbleSpeed
            };

            bubbleShooter.Charge();
        }

        private void CreateGameStates(Map map, Level level, GameServices gameServices)
        {
            var gameOverState = new GameOverGameState(
                new GameOverGameState.Settings
                {
                    Map = map,
                    BubbleExploder = level.BubbleExploder,
                    MinBubblePositionToActive = gameServices.GameConfigService.BubbleMinYPositionToGameOver,
                    UserInputService = gameServices.UserInputService,
                    SceneService = gameServices.SceneService
                }
            );

            var gameVictoryGameState = new GameVictoryGameState(
                new GameVictoryGameState.Settings
                {
                    Map = map,
                    Level = level,
                    UserInputService = gameServices.UserInputService,
                    SceneService = gameServices.SceneService
                }
            );

            var pauseGameState = new PauseGameState(
                new PauseGameState.Settings
                {
                    UserInputService = gameServices.UserInputService,
                    UpdateService = gameServices.UpdateService,
                    SceneService = gameServices.SceneService
                }
            );

            gameServices.ViewModelService.ConstructViewModel(gameOverState);
            gameServices.ViewModelService.ConstructViewModel(gameVictoryGameState);
            gameServices.ViewModelService.ConstructViewModel(pauseGameState);
        }
    }
}