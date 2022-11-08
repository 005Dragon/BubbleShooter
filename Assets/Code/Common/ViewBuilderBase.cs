using System;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Common
{
    public abstract class ViewBuilderBase<TModel, TView> : IViewBuilder
        where TModel : IModel
        where TView : MonoBehaviour, IView
    {
        public Type ModelType => typeof(TModel);

        public void Build(IModel model)
        {
            if (model.GetType() != ModelType)
            {
                throw new Exception("This builder not be use for this model.");
            }

            var typedModel = (TModel)model;

            if (!TryGetExistingView(typedModel, out TView view))
            {
                var gameObject = new GameObject();
                view = CreateView(gameObject, typedModel);
            }
            
            view.Initialize(model);
        }

        protected virtual TView CreateView(GameObject gameObject, TModel model)
        {
            var view = gameObject.AddComponent<TView>();
            return view;
        }

        protected virtual bool TryGetExistingView(TModel model, out TView view)
        {
            view = default;
            return false;
        }
    }
}