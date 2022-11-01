using System;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Builders
{
    public class GameOverViewBuilder : ViewBuilderBase<GameOver, GameOverView>
    {
        private readonly Func<GameOverView> _getGameOverView;

        public GameOverViewBuilder(Transform root, Func<GameOverView> getGameOverView) : base(root)
        {
            _getGameOverView = getGameOverView;
        }

        protected override GameOverView CreateView(GameObject gameObject, GameOver model)
        {
            var view = gameObject.AddComponent<GameOverView>();

            view.Model = model;

            return view;
        }

        protected override bool TryGetViewFromPool(GameOver model, out GameOverView view)
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