using System;
using Code.Common;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using Code.MainMenuScene.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenuScene.Views
{
    public class MainMenuGameStateView : ViewBase<MainMenuGameState>
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private ChangeLevelView _changeLevelView;

        public override void Initialize(IModel model)
        {
            base.Initialize(model);
            
            _startGameButton.onClick.AddListener(Model.LoadGameScene);
            _changeLevelView.ChangingLevel += ChangeLevelViewOnChangingLevel;
        }

        protected override void FreeView()
        {
            _startGameButton.onClick.RemoveListener(Model.LoadGameScene);
            _changeLevelView.ChangingLevel -= ChangeLevelViewOnChangingLevel;
            
            base.FreeView();
        }

        private void ChangeLevelViewOnChangingLevel(object sender, EventArgs eventArgs)
        {
            LevelKey currentLevel = Model.ChangeLevel();
            _changeLevelView.SetLevel(currentLevel);
        }
    }
}