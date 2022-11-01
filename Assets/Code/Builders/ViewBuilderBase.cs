using System;
using Code.Models;
using Code.Views;
using UnityEngine;

namespace Code.Builders
{
    public abstract class ViewBuilderBase<TModel, TView> : IViewBuilder
        where TModel : IModel
        where TView : MonoBehaviour, IView
    {
        public Type ModelType => typeof(TModel);
        
        private readonly Transform _root;

        protected ViewBuilderBase(Transform root)
        {
            _root = root;
        }

        public IView Build(IModel model)
        {
            if (model.GetType() != ModelType)
            {
                throw new Exception("This builder not be use for this model.");
            }

            var typedModel = (TModel)model;

            if (!TryGetViewFromPool(typedModel, out TView view))
            {
                var gameObject = new GameObject();
                gameObject.transform.SetParent(_root);

                view = CreateView(gameObject, typedModel);
            }
            
            view.Initialize();

            return view;
        }

        protected abstract TView CreateView(GameObject gameObject, TModel model);

        protected virtual bool TryGetViewFromPool(TModel model, out TView view)
        {
            view = default;
            return false;
        }
    }
}