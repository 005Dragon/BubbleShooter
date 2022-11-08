using System;

namespace Code.Infrastructure.Models
{
    public abstract class ModelBuilderBase<TModel, TSettings> : IModelBuilder
        where TSettings : ModelSettingsBase
        where TModel : ModelBase<TSettings>
    {
        public Type ModelType => typeof(TModel);
        
        public IModel Build(ModelSettingsBase settings)
        {
            if (settings is not TSettings modelSettings)
            {
                throw new Exception(nameof(settings) + " must be type: " + typeof(TSettings) + ".");
            }

            return Build(modelSettings);
        }

        protected abstract TModel Build(TSettings settings);
    }
}