using System;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Builders
{
    public class BubbleShooterAimViewBuilder : ViewBuilderBase<BubbleShooterAim, BubbleShooterAimView>
    {
        private readonly Func<BubbleShooterAimView> _getView;

        public BubbleShooterAimViewBuilder(Transform root, Func<BubbleShooterAimView> getView) : base(root)
        {
            _getView = getView;
        }

        protected override bool TryGetExistingView(BubbleShooterAim model, out BubbleShooterAimView view)
        {
            view = _getView();

            if (view != null)
            {
                view.Model = model;
                return true;
            }

            return false;
        }
    }
}