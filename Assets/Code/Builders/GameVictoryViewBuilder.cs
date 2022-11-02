using System;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Builders
{
    public class GameVictoryViewBuilder : ViewBuilderBase<GameVictory, GameVictoryView>
    {
        private readonly Func<GameVictoryView> _getGameVictoryView;

        public GameVictoryViewBuilder(Transform root, Func<GameVictoryView> getGameVictoryView) : base(root)
        {
            _getGameVictoryView = getGameVictoryView;
        }

        protected override bool TryGetExistingView(GameVictory model, out GameVictoryView view)
        {
            view = _getGameVictoryView();

            if (view != null)
            {
                view.Model = model;
                return true;
            }

            return false;
        }
    }
}