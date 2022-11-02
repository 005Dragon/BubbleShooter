using Code.GameScene.Models;
using UnityEngine;

namespace Code.GameScene.Views
{
    public class GameOverMinHeightLineView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private Transform _cachedTransform;
        
        public void Initialize()
        {
            _cachedTransform = transform;
            var model = (GameOver.MinHeightLine)Model;
            
            model.PositionChanged += ModelOnPositionChanged;
        }

        private void ModelOnPositionChanged(object sender, float position)
        {
            _cachedTransform.position = new Vector2(_cachedTransform.position.x, position);
        }
    }
}