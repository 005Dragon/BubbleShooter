using System.Collections.Generic;
using Code.Builders;
using Code.Common;
using Code.Models;
using Code.Movers;
using Code.Services;
using Code.Storage;
using Code.Views;
using UnityEngine;

namespace Code
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

        private UserInput _userInput;
        private BubbleWayBuilder _debugBubbleWayBuilder;

        private void Awake()
        {
            var updateBehavior = GetComponent<UpdateBehavior>();
            
            var bubbleService = new BubbleService(_bubbleViewStorage);
            var levelService = new LevelService(_levelStorage);

            var viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders(bubbleService));
            var bubbleMoverDispatcher = new BubbleMoverDispatcher();
            _userInput = new UserInput(_mainCamera);
            
            updateBehavior.AddToUpdate(_userInput);
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

            _debugBubbleWayBuilder = new BubbleWayBuilder(map, 4);

            var bubbleShooter = new BubbleShooter(map, _userInput, bubbleMoverDispatcher, bubbleBuilder, bubbleExploder)
            {
                Position = _bubbleShooterSpawner.position,
                BubbleMoveSpeed = _bubbleSpeed
            };
            
            bubbleShooter.Charge();
            
            GameOver gameOver = 
                new GameOverBuilder(map, bubbleExploder, viewModelDispatcher, x => updateBehavior.StopUpdate = x)
                    .Build();

            gameOver.MinBubblePositionToActive = _bubbleMinYPositionToGameOver;
        }

        // TODO delete after debug.
        private void Update()
        {
            if (_userInput.Aiming)
            {
                Vector2 direction = 
                    (_userInput.GetTargetPosition() - (Vector2)_bubbleShooterSpawner.position).normalized;
            
                List<Vector2> points =
                    _debugBubbleWayBuilder.Build(_bubbleShooterSpawner.position, direction);

                Vector2 lastPoint = _bubbleShooterSpawner.position;

                foreach (Vector2 point in points)
                {
                    Debug.DrawLine(lastPoint, point, Color.black);
                    lastPoint = point;
                }
            }

            Debug.DrawLine(_bubbleShooterSpawner.position, _userInput.GetTargetPosition(), Color.red);
        }

        private IViewBuilder[] GetViewBuilders(BubbleService bubbleService)
        {
            Transform cachedTransform = transform;
            
            return new IViewBuilder[]
            {
                new BubbleViewBuilder(cachedTransform, bubbleService),
                new GameOverViewBuilder(cachedTransform, FindObjectOfType<GameOverView>),
                new GameOverMinHeightLineViewBuilder(cachedTransform, FindObjectOfType<GameOverMinHeightLineView>)
            };
        }
    }
}