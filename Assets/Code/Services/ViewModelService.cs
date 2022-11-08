using System;
using System.Collections.Generic;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;

namespace Code.Services
{
    public class ViewModelService
    {
        private readonly Dictionary<Type, IViewBuilder> _modelTypeToViewBuilderIndex = new();
        private readonly Dictionary<Type, IModelBuilder> _modelTypeToModelBuilderIndex = new();

        public TModel ConstructViewModel<TModel, TSettings>(TSettings settings)
            where TSettings : ModelSettingsBase
            where TModel : ModelBase<TSettings>
        {
            var model = (TModel)_modelTypeToModelBuilderIndex[typeof(TModel)].Build(settings);

            ConstructViewModel(model);

            return model;
        }

        public void ConstructViewModel(IModel model)
        {
            _modelTypeToViewBuilderIndex[model.GetType()].Build(model);
        }

        public void Register(IViewBuilder viewBuilder)
        {
            _modelTypeToViewBuilderIndex[viewBuilder.ModelType] = viewBuilder;
        }

        public void Register(IModelBuilder modelBuilder)
        {
            _modelTypeToModelBuilderIndex[modelBuilder.ModelType] = modelBuilder;
        }
    }
}