using Code.Common;
using Code.GameScene.Models;
using UnityEngine;

namespace Code.GameScene.Views
{
    public class GameOverView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private GameObject _gameObject;
        
        public void Initialize()
        {
            _gameObject = gameObject;
            _gameObject.SetActive(false);
            
            var model = (GameOver)Model;
            
            model.ActiveGameOverChanged += ModelOnActiveGameOverChanged;
        }

        private void ModelOnActiveGameOverChanged(object sender, bool active)
        {
            _gameObject.SetActive(active);
        }
    }
}