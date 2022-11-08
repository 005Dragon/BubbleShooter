using Code.GameScene.Models;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameScene.Views
{
    public class PauseGameStateView : ViewBase<PauseGameState>
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _reloadGameButton;
        [SerializeField] private Button _backToMenuButton;
        
        public override void Initialize(IModel model)
        {
            base.Initialize(model);
            
            _continueButton.onClick.AddListener(ChangeActivePause);
            _reloadGameButton.onClick.AddListener(Model.ReloadGameScene);
            _backToMenuButton.onClick.AddListener(Model.LoadMainMenuScene);
            Model.ActivePauseChanged += ModelOnActivePauseChanged;
        }
        
        public void ChangeActivePause() => Model.ChangeActivePause();

        protected override void FreeView()
        {
            _continueButton.onClick.RemoveListener(ChangeActivePause);
            _reloadGameButton.onClick.RemoveListener(Model.ReloadGameScene);
            _backToMenuButton.onClick.RemoveListener(Model.LoadMainMenuScene);
            Model.ActivePauseChanged -= ModelOnActivePauseChanged;
            
            base.FreeView();
        }

        private void ModelOnActivePauseChanged(object sender, bool active)
        {
            CachedGameObject.SetActive(active);
        }
    }
}