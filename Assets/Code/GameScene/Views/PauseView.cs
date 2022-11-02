using Code.Common;
using Code.GameScene.Models;
using UnityEngine;

namespace Code.GameScene.Views
{
    public class PauseView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private GameObject _gameObject;
        private Pause _model;
        
        public void Initialize()
        {
            _gameObject = gameObject;
            _gameObject.SetActive(false);
            
            _model = (Pause)Model;
            
            _model.ActivePauseChanged += ModelOnActivePauseChanged;
        }

        public void ChangeActivePause() => _model.ChangeActivePause();

        private void ModelOnActivePauseChanged(object sender, bool active)
        {
            _gameObject.SetActive(active);
        }
    }
}