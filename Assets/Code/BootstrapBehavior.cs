using System.Collections.Generic;
using Code.Builders;
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
        [SerializeField] private Transform _bubbleShooterSpawner;
        [SerializeField] private float _bubbleSpeed;
        [SerializeField] private float _bubbleDiameter;

        [Header("Storages")] 
        [SerializeField] private BubbleViewStorage _bubbleViewStorage;

        private UserInput _userInput;
        private BubbleWayBuilder _debugBubbleWayBuilder;

        private void Awake()
        {
            var updateBehavior = GetComponent<UpdateBehavior>();
            
            var bubbleService = new BubbleService(_bubbleViewStorage);

            var viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders(bubbleService));
            var bubbleMoverDispatcher = new BubbleMoverDispatcher();
            _userInput = new UserInput(_mainCamera);
            
            updateBehavior.AddToUpdate(_userInput);
            updateBehavior.AddToUpdate(bubbleMoverDispatcher);
            
            var map = new Map(_mainCamera);
            map.Construct(new Vector2(_bubbleDiameter, _bubbleDiameter));

            _debugBubbleWayBuilder = new BubbleWayBuilder(map, 4);

            var bubbleShooter = new BubbleShooter(map, _userInput, bubbleMoverDispatcher, viewModelDispatcher)
            {
                Position = _bubbleShooterSpawner.position,
                BubbleMoveSpeed = _bubbleSpeed,
                BubbleDiameter = _bubbleDiameter
            };
            
            bubbleShooter.Charge();
            
            new GameOverBuilder(map, viewModelDispatcher, x => updateBehavior.StopUpdate = x).Build();
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
                new GameOverViewBuilder(cachedTransform, FindObjectOfType<GameOverView>)
            };
        }
    }
}