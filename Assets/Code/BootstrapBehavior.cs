using System;
using UnityEngine;

namespace Code
{
    public class BootstrapBehavior : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;

        [Header("Storages")] 
        [SerializeField] private BubbleViewStorage _bubbleViewStorage;

        private BubbleService _bubbleService;
        private UserInput _userInput;

        private void Awake()
        {
            _bubbleService = new BubbleService(_bubbleViewStorage);

            _userInput = new UserInput(_mainCamera);
        }

        public void Update()
        {
            _userInput.Update();
        }
    }
}