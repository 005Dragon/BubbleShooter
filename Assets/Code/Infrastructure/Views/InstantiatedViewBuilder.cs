using System;
using System.Linq;
using Code.Infrastructure.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Views
{
    public class InstantiatedViewBuilder<TModel, TView> : IViewBuilder
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

            TView view = Object.FindObjectsOfType<TView>(true).FirstOrDefault(x => !x.Initialized);

            if (view == null)
            {
                throw new Exception("Not found view to " + typeof(TModel) + ".");
            }
            
            view.Initialize(model);
        }
    }
}