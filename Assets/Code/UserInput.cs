using System;
using UnityEngine;

namespace Code
{
    public class UserInput : IUpdatable
    {
        public event EventHandler<Vector2> Shot;
        
        public bool Aiming { get; private set; }
        
        private readonly Camera _camera;

        public UserInput(Camera camera)
        {
            _camera = camera;
        }

        public bool Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Aiming = true;
            }
            
            if (Aiming && Input.GetMouseButtonUp(0))
            {
                Shot?.Invoke(this, GetTargetPosition());
                Aiming = false;
            }

            return true;
        }

        public Vector2 GetTargetPosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}