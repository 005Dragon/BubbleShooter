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

        [Header("Storages")] 
        [SerializeField] private BubbleViewStorage _bubbleViewStorage;

        private BubbleService _bubbleService;
        private UserInput _userInput;
        private UpdateBehavior _updateBehavior;
        private BubbleBuilder _bubbleBuilder;

        private void Awake()
        {
            _updateBehavior = GetComponent<UpdateBehavior>();
            
            _bubbleService = new BubbleService(_bubbleViewStorage);

            ViewModelDispatcher viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders());
            
            _userInput = new UserInput(_mainCamera);
            _updateBehavior.AddToUpdate(_userInput);

            _bubbleBuilder = new BubbleBuilder(viewModelDispatcher);
            
            _userInput.Shot += UserInputOnShot;
        }

        private void UserInputOnShot(object sender, Vector2 target)
        {
            _bubbleBuilder.Build(BubbleType.Yellow, target);
        }

        private IViewBuilder[] GetViewBuilders()
        {
            return new IViewBuilder[]
            {
                new BubbleViewBuilder(transform, _bubbleService)
            };
        }
    }
}