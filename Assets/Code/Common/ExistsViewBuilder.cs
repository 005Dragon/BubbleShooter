using System;
using UnityEngine;

namespace Code.Common
{
    public class ExistsViewBuilder<TModel, TView> : ViewBuilderBase<TModel, TView>
        where TModel : IModel
        where TView : MonoBehaviour, IView
    {
        private readonly Func<TView> _getView;

        public ExistsViewBuilder(Transform root, Func<TView> getView) : base(root)
        {
            _getView = getView;
        }

        protected override bool TryGetExistingView(TModel model, out TView view)
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