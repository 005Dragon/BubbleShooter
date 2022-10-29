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
        [SerializeField] private float _mapWidth;

        [Header("Storages")] 
        [SerializeField] private BubbleViewStorage _bubbleViewStorage;

        private void Awake()
        {
            var updateBehavior = GetComponent<UpdateBehavior>();
            
            var bubbleService = new BubbleService(_bubbleViewStorage);

            ViewModelDispatcher viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders(bubbleService));
            
            var userInput = new UserInput(_mainCamera);
            updateBehavior.AddToUpdate(userInput);

            var map = new Map(_mainCamera);
            map.Construct(_mapWidth);

            var bubbleBuilder = new BubbleBuilder(viewModelDispatcher);
            var bubbleMoveBuilder = new BubbleMoveBuilder(_bubbleSpeed, map, updateBehavior);
            
            new BubbleShooter(
                _bubbleShooterSpawner.position,
                bubbleBuilder,
                bubbleMoveBuilder,
                userInput
            );
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