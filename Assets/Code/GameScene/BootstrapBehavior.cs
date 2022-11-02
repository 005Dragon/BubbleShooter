using Code.GameScene.Builders;
using Code.GameScene.Common;
using Code.GameScene.Models;
using Code.GameScene.Movers;
using Code.GameScene.Services;
using Code.GameScene.Storage;
using Code.GameScene.Views;
using UnityEngine;

namespace Code.GameScene
{
    [RequireComponent(typeof(UpdateBehavior))]
    public class BootstrapBehavior : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _mainCamera;

        [Header("Settings")] 
        [SerializeField] private LevelKey _level;
        [SerializeField] private Transform _bubbleShooterSpawner;
        [SerializeField] private float _bubbleSpeed;
        [SerializeField] private float _bubbleDiameter;
        [SerializeField] private float _bubbleMinYPositionToGameOver;

        [Header("Storages")] 
        [SerializeField] private BubbleViewStorage _bubbleViewStorage;
        [SerializeField] private LevelStorage _levelStorage;

        private void Awake()
        {
            var updateBehavior = GetComponent<UpdateBehavior>();
            
            var bubbleService = new BubbleService(_bubbleViewStorage);
            var levelService = new LevelService(_levelStorage);

            var viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders(bubbleService));
            var bubbleMoverDispatcher = new BubbleMoverDispatcher();
            var userInput = new UserInput(_mainCamera);
            
            updateBehavior.AddToUpdate(userInput);
            updateBehavior.AddToUpdate(bubbleMoverDispatcher);

            var map = new Map(_mainCamera);
            map.Construct(new Vector2(_bubbleDiameter, _bubbleDiameter));
            
            var bubbleBuilder = new BubbleBuilder(viewModelDispatcher)
            {
                Diameter = _bubbleDiameter
            };
            var bubbleExploder = new BubbleExploder(map, bubbleMoverDispatcher);

            var level = new Level(levelService, map, bubbleBuilder, bubbleMoverDispatcher, bubbleExploder);
            level.Construct(_level);

            Vector2 bubbleShooterPosition = _bubbleShooterSpawner.position;
            new BubbleShooterAimBuilder(viewModelDispatcher, map, userInput, updateBehavior.AddToUpdate)
                .Build(bubbleShooterPosition, 3);

            var bubbleShooter = new BubbleShooter(map, userInput, bubbleMoverDispatcher, bubbleBuilder, bubbleExploder)
            {
                Position = bubbleShooterPosition,
                BubbleMoveSpeed = _bubbleSpeed
            };
            
            bubbleShooter.Charge();
            
            GameOver gameOver = 
                new GameOverBuilder(map, bubbleExploder, viewModelDispatcher, x => updateBehavior.StopUpdate = x)
                    .Build();

            gameOver.MinBubblePositionToActive = _bubbleMinYPositionToGameOver;

            new GameVictoryBuilder(map, level, x => updateBehavior.StopUpdate = x, viewModelDispatcher).Build();
        }

        private IViewBuilder[] GetViewBuilders(BubbleService bubbleService)
        {
            Transform cachedTransform = transform;
            
            return new IViewBuilder[]
            {
                new BubbleViewBuilder(cachedTransform, bubbleService),
                new GameOverViewBuilder(cachedTransform, FindObjectOfType<GameOverView>),
                new GameOverMinHeightLineViewBuilder(cachedTransform, FindObjectOfType<GameOverMinHeightLineView>),
                new BubbleShooterAimViewBuilder(cachedTransform, FindObjectOfType<BubbleShooterAimView>),
                new GameVictoryViewBuilder(cachedTransform, FindObjectOfType<GameVictoryView>)
            };
        }
    }
}