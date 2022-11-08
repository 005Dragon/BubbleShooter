using Code.GameScene.Models;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameScene.Views
{
    public class GameOverGameStateView : ViewBase<GameOverGameState>
    {
        [SerializeField] private Transform _redLine;
        [SerializeField] private Button _reloadGameButton;
        [SerializeField] private Button _backToMenuButton;
        
        public override void Initialize(IModel model)
        {
            base.Initialize(model);

            _redLine.position = new Vector2(_redLine.position.x, Model.MinBubblePositionToActive);
            
            _reloadGameButton.onClick.AddListener(Model.ReloadGameScene);
            _backToMenuButton.onClick.AddListener(Model.LoadMainMenuScene);
            Model.ActiveGameOverChanged += ModelOnActiveGameOverChanged;
        }

        private void ModelOnActiveGameOverChanged(object sender, bool active)
        {
            CachedGameObject.SetActive(active);
        }

        protected override void FreeView()
        {
            _reloadGameButton.onClick.RemoveListener(Model.ReloadGameScene);
            _backToMenuButton.onClick.RemoveListener(Model.LoadMainMenuScene);
            Model.ActiveGameOverChanged -= ModelOnActiveGameOverChanged;
            
            base.FreeView();
        }
    }
}