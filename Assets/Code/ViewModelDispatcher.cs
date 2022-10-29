using System;
using System.Collections.Generic;

namespace Code
{
    public class ViewModelDispatcher
    {
        private readonly Dictionary<Type, IViewBuilder> _modelTypeToViewBuilderIndex = new();
        private readonly List<IView> _views = new();

        public ViewModelDispatcher(IViewBuilder[] viewBuilders)
        {
            foreach (IViewBuilder viewBuilder in viewBuilders)
            {
                _modelTypeToViewBuilderIndex[viewBuilder.ModelType] = viewBuilder;
            }
        }
        
        public void ConstructViewModel(IModel model)
        {
            if (_views.Exists(x => x.Model == model))
            {
                return;
            }

            IViewBuilder viewBuilder = _modelTypeToViewBuilderIndex[model.GetType()];
            IView view = viewBuilder.Build(model);
            _views.Add(view);
        }
    }
}