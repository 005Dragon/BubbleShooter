using System;
using UnityEngine;

namespace Code
{
    public class UserInput : IUpdatable
    {
        public event EventHandler<Vector2> Shot;
        
        private readonly Camera _camera;

        public UserInput(Camera camera)
        {
            _camera = camera;
        }

        public bool Update()
        {
            if (Input.GetMouseButton(0))
            {
                Shot?.Invoke(this, GetTargetPosition());
            }

            return true;
        }

        public Vector2 GetTargetPosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}