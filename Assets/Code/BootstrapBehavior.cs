using System.Collections.Generic;
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

        private BubbleWayBuilder _bubbleWayBuilder;
        private UserInput _userInput;

        private void Awake()
        {
            var updateBehavior = GetComponent<UpdateBehavior>();
            
            var bubbleService = new BubbleService(_bubbleViewStorage);

            ViewModelDispatcher viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders(bubbleService));
            
            _userInput = new UserInput(_mainCamera);
            updateBehavior.AddToUpdate(_userInput);
            
            var map = new Map(_mainCamera);
            map.Construct(new Vector2(_bubbleDiameter, _bubbleDiameter));

            var bubbleBuilder = new BubbleBuilder(_bubbleDiameter, viewModelDispatcher);
            var bubbleMoveBuilder = new BubbleMoveBuilder(_bubbleSpeed, updateBehavior);
            _bubbleWayBuilder = new BubbleWayBuilder(map);

            for (int ii = 0; ii < 3; ii++)
            {
                for (int i = 0; i < map.GridSize.x; i++)
                {
                    map.AttachToGrid(new Vector2Int(i, ii), bubbleBuilder.Build(BubbleService.GetRandomType()));
                }
            }

            new BubbleShooter(
                _bubbleShooterSpawner.position,
                bubbleBuilder,
                bubbleMoveBuilder,
                _bubbleWayBuilder,
                map,
                _userInput
            );
        }

        // TODO delete after debug.
        private void Update()
        {
            Vector2 direction = (_userInput.GetTargetPosition() - (Vector2)_bubbleShooterSpawner.position).normalized;
            
            List<Vector2> points =
                _bubbleWayBuilder.Build(_bubbleShooterSpawner.position, direction, 3);

            Vector2 lastPoint = _bubbleShooterSpawner.position;

            foreach (Vector2 point in points)
            {
                Debug.DrawLine(lastPoint, point, Color.black);
                lastPoint = point;
            }
            
            Debug.DrawLine(_bubbleShooterSpawner.position, _userInput.GetTargetPosition(), Color.red);
        }

        private IViewBuilder[] GetViewBuilders(BubbleService bubbleService)
        {
            return new IViewBuilder[]
            {
                new BubbleViewBuilder(transform, bubbleService)
            };
        }
    }
}