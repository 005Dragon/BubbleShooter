using System;
using UnityEngine;

namespace Code
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
            
            var gameObject = new GameObject();
            gameObject.transform.SetParent(_root);

            TView view = CreateView(gameObject);

            view.Model = model;

            return view;
        }

        protected abstract TView CreateView(GameObject gameObject);
    }
}