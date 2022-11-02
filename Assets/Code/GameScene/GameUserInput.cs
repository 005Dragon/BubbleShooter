using System;
using Code.Common;
using UnityEngine;

namespace Code.GameScene
{
    public class GameUserInput : IUpdatable
    {
        public event EventHandler<Vector2> Shot;
        public event EventHandler Pause;
        
        public bool Aiming { get; private set; }
        
        private readonly Camera _camera;

        public GameUserInput(Camera camera)
        {
            _camera = camera;
        }

        public bool Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Pause?.Invoke(this, EventArgs.Empty);
            }
            
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