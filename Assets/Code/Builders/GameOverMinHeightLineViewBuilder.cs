using System;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Builders
{
    public class GameOverMinHeightLineViewBuilder : ViewBuilderBase<GameOver.MinHeightLine, GameOverMinHeightLineView>
    {
        private readonly Func<GameOverMinHeightLineView> _getGameOverMinHeightLineView;

        public GameOverMinHeightLineViewBuilder(
            Transform root,
            Func<GameOverMinHeightLineView> getGameOverMinHeightLineView)
            : base(root)
        {
            _getGameOverMinHeightLineView = getGameOverMinHeightLineView;
        }

        protected override bool TryGetExistingView(GameOver.MinHeightLine model, out GameOverMinHeightLineView view)
        {
            view = _getGameOverMinHeightLineView();

            if (view != null)
            {
                view.Model = model;
                return true;
            }

            return false;
        }
    }
}