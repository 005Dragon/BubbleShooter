using System;
using Code.GameScene.Models;
using Code.GameScene.Views;
using UnityEngine;

namespace Code.GameScene.Builders
{
    public class GameOverViewBuilder : ViewBuilderBase<GameOver, GameOverView>
    {
        private readonly Func<GameOverView> _getGameOverView;

        public GameOverViewBuilder(Transform root, Func<GameOverView> getGameOverView) : base(root)
        {
            _getGameOverView = getGameOverView;
        }

        protected override bool TryGetExistingView(GameOver model, out GameOverView view)
        {
            view = _getGameOverView();

            if (view != null)
            {
                view.Model = model;
                return true;
            }

            return false;
        }
    }
}