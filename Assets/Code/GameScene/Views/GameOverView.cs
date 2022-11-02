using Code.GameScene.Models;
using UnityEngine;

namespace Code.GameScene.Views
{
    public class GameOverView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private GameOver _model;
        private GameObject _gameObject;
        
        public void Initialize()
        {
            _gameObject = gameObject;
            _model = (GameOver)Model;
            
            _gameObject.SetActive(false);
            
            _model.ActiveGameOverChanged += ModelOnActiveGameOverChanged;
        }

        public void ReloadScene() => _model.ReloadScene();

        private void ModelOnActiveGameOverChanged(object sender, bool active)
        {
            _gameObject.SetActive(active);
        }
    }
}