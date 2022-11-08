using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services
{
    public class GameUserInputService
    {
        public event EventHandler<Vector2> Shot;
        public event EventHandler Pause;
        
        public bool Aiming { get; private set; }
        
        private readonly Camera _camera;
        private readonly Controls _controls;

        public GameUserInputService(Camera camera)
        {
            _camera = camera;

            _controls = new Controls();
            
            _controls.Main.Pause.performed += PauseOnPerformed;
            _controls.Main.Shot.performed += AimingOnPerformed;
            _controls.Main.Shot.canceled += AimingOnCanceled; 
        }

        private void AimingOnPerformed(InputAction.CallbackContext obj)
        {
            Aiming = true;
        }

        private void AimingOnCanceled(InputAction.CallbackContext obj)
        {
            Aiming = false;
            Shot?.Invoke(this, GetTargetPosition());
        }

        private void PauseOnPerformed(InputAction.CallbackContext obj)
        {
            Pause?.Invoke(this, EventArgs.Empty);
        }

        public void Enable() => _controls.Enable();
        public void Disable() => _controls.Disable();

        public Vector2 GetTargetPosition()
        {
            Vector2 targetPosition = default;
            
            if (Touchscreen.current != null)
            {
                targetPosition = Touchscreen.current.position.ReadValue();
            }

            if (Mouse.current != null)
            {
                targetPosition = Mouse.current.position.ReadValue();
            }
            
            return _camera.ScreenToWorldPoint(targetPosition);
        }
    }
}