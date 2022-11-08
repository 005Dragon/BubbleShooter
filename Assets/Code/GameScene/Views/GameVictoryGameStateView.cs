using Code.GameScene.Models;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameScene.Views
{
    public class GameVictoryGameStateView : ViewBase<GameVictoryGameState>
    {
        [SerializeField] private Button _backToMenuButton;
        
        public override void Initialize(IModel model)
        {
            base.Initialize(model);
            
            _backToMenuButton.onClick.AddListener(Model.LoadMainMenuScene);
            Model.ActiveGameOverChanged += ModelOnActiveGameOverChanged;
        }

        protected override void FreeView()
        {
            _backToMenuButton.onClick.RemoveListener(Model.LoadMainMenuScene);
            Model.ActiveGameOverChanged -= ModelOnActiveGameOverChanged;
            
            base.FreeView();
        }

        private void ModelOnActiveGameOverChanged(object sender, bool active)
        {
            CachedGameObject.SetActive(active);
        }
    }
}