using Code.Common;
using Code.Common.Models;
using Code.Common.Views;
using Code.GameScene.Models;
using Code.GameScene.Models.Builders;
using Code.GameScene.Movers;
using Code.GameScene.Views;
using Code.GameScene.Views.Builders;
using Code.Services;
using Code.Storage;
using UnityEngine;

namespace Code.GameScene
{
    [RequireComponent(typeof(UpdateBehavior))]
    public class BootstrapGameBehavior : MonoBehaviour
    {
        [SerializeField] private GameConfigData _gameConfig;

        [Header("Storages")] 
        [SerializeField] private BubbleViewStorage _bubbleViewStorage;
        [SerializeField] private LevelStorage _levelStorage;
        [SerializeField] private SceneStorage _sceneStorage;

        private void Awake()
        {
            var mainCamera = Camera.main;
            var updateBehavior = GetComponent<UpdateBehavior>();
            
            var bubbleService = new BubbleService(_bubbleViewStorage);
            var levelService = new LevelService(_levelStorage);
            var sceneService = new SceneService(_sceneStorage);

            var viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders(bubbleService));
            var bubbleMoverDispatcher = new BubbleMoverDispatcher();
            var userInput = new GameUserInput(mainCamera);
            
            updateBehavior.AddToUpdate(userInput);
            updateBehavior.AddToUpdate(bubbleMoverDispatcher);

            var map = new Map(mainCamera);
            map.Construct(new Vector2(_gameConfig.BubbleDiameter, _gameConfig.BubbleDiameter));
            
            var bubbleBuilder = new BubbleBuilder(viewModelDispatcher)
            {
                Diameter = _gameConfig.BubbleDiameter
            };
            var bubbleExploder = new BubbleExploder(map, bubbleMoverDispatcher);

            var level = new Level(levelService, map, bubbleBuilder, bubbleMoverDispatcher, bubbleExploder);
            level.Construct(_gameConfig.Level);

            new BubbleShooterAimBuilder(viewModelDispatcher, map, userInput, updateBehavior.AddToUpdate)
                .Build(_gameConfig.BubbleShooterPosition, 3);

            var bubbleShooter = new BubbleShooter(map, userInput, bubbleMoverDispatcher, bubbleBuilder, bubbleExploder)
            {
                Position = _gameConfig.BubbleShooterPosition,
                BubbleMoveSpeed = _gameConfig.BubbleSpeed
            };
            
            bubbleShooter.Charge();

            GameOver gameOver = new GameOverBuilder(
                    map,
                    bubbleExploder,
                    viewModelDispatcher,
                    x => updateBehavior.StopUpdate = x,
                    sceneService)
                .Build();

            gameOver.MinBubblePositionToActive = _gameConfig.BubbleMinYPositionToGameOver;

            new GameVictoryBuilder(map, level, x => updateBehavior.StopUpdate = x, viewModelDispatcher, sceneService)
                .Build();
        }

        private IViewBuilder[] GetViewBuilders(BubbleService bubbleService)
        {
            Transform cachedTransform = transform;

            return new IViewBuilder[]
            {
                new BubbleViewBuilder(cachedTransform, bubbleService),
                new ExistsViewBuilder<GameOver, GameOverView>(cachedTransform, FindObjectOfType<GameOverView>),
                new ExistsViewBuilder<GameVictory, GameVictoryView>(cachedTransform, FindObjectOfType<GameVictoryView>),
                new ExistsViewBuilder<BubbleShooterAim, BubbleShooterAimView>(
                    cachedTransform,
                    FindObjectOfType<BubbleShooterAimView>
                ),

                new ExistsViewBuilder<GameOver.MinHeightLine, GameOverMinHeightLineView>(
                    cachedTransform,
                    FindObjectOfType<GameOverMinHeightLineView>
                ),

                new ExistsViewBuilder<StartGameNavigationPoint, StartGameNavigationPointView>(
                    cachedTransform,
                    FindObjectOfType<StartGameNavigationPointView>
                ),

                new ExistsViewBuilder<GameOverMainMenuNavigationPoint, GameOverMainMenuNavigationPointView>(
                    cachedTransform,
                    FindObjectOfType<GameOverMainMenuNavigationPointView>
                ),

                new ExistsViewBuilder<GameVictoryMainMenuNavigationPoint, GameVictoryMainMenuNavigationPointView>
                (
                    cachedTransform,
                    FindObjectOfType<GameVictoryMainMenuNavigationPointView>
                )
            };
        }
    }
}