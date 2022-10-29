using System;
using UnityEngine;

namespace Code
{
    public class UserInput
    {
        public event EventHandler<Vector2> Shot;
        
        private readonly Camera _camera;

        public UserInput(Camera camera)
        {
            _camera = camera;
        }

        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Shot?.Invoke(this, GetTargetPosition());
            }
        }

        public Vector2 GetTargetPosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}